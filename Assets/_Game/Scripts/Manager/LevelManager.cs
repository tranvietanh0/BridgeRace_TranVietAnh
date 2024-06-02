using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class LevelManager : GOSingleton<LevelManager>
{
    [SerializeField] public Transform firstPosPlayer;
    public Player player;
    public Bot bot;
    public Platform platform;

    private List<ColorType> colorTypes = new List<ColorType>() {ColorType.Red, ColorType.Green, ColorType.Yellow, ColorType.Orange, ColorType.Purple, ColorType.Black };

    public List<ColorType> colorRandoms = new List<ColorType>();
    private List<Bot> bots = new List<Bot>();
    public List<LevelController> levelPrefabs = new List<LevelController>();
    [SerializeField] private LevelController currentLevel;
    public List<Brick> BrickAfterChangeColor => platform.brickAfterChangeColor;

    [SerializeField] private List<Transform> finishPosList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(Pref.curPlayerLevel);
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void OnInit()
    {
        platform.ClearBrickPositions();
        //Set finishPos cho bot o moi level
        bot.finishPos.position = levelPrefabs[Pref.curPlayerLevel].finishPos.position;
        Debug.Log(bot.finishPos.position);
        // set vi tri cho player va bot
        List<Vector3> characterPos = new List<Vector3>();
        Vector3 startPos = firstPosPlayer.position;
        for (int i = 0; i < 3; i++)
        {
            startPos += Vector3.right * 2f;
            characterPos.Add(startPos);
        }

        //random 4 mau gach o moi level
        while (colorRandoms.Count < 4)
        {
            int indexColorData = Random.Range(0, colorTypes.Count);
            if (!colorRandoms.Contains(colorTypes[indexColorData]))
            {
                colorRandoms.Add(colorTypes[indexColorData]);
            }
        }
        //set up platform
        platform.OnEmptyPoint();
        platform.EnableBrick();
        platform.ChangeColorBrickOnStage(colorRandoms);
            // platform.TakeColor(colorRandoms[randomColorIndex]);
        //set mau cho player
        player.OnInit();
        int randomIndexPlayerColor = Random.Range(0, colorRandoms.Count);
        player.ChangeColor(colorRandoms[randomIndexPlayerColor]);
        colorRandoms.RemoveAt(randomIndexPlayerColor);

        for (int i = 0; i < 3; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, characterPos[i], Quaternion.identity);
            bot.OnInit();
            bot.ChangeColor(colorRandoms[i]);
            bots.Add(bot);
        }
    }

    public void EnterBotPlay()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new IdleState());
        }
    }
    public void LoadLevel(int indexOfLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (indexOfLevel < levelPrefabs.Count)
        {
            currentLevel = Instantiate(levelPrefabs[indexOfLevel]);
        }
        else
        {
            
        }
    }
    public void ResetPoolingObject()
    {
        SimplePool.CollectAll();
        bots.Clear();
        
    }
    public void NextLevel()
    {
        Pref.curPlayerLevel++;
        player.RemoveAllBrick();
        player.transform.position = firstPosPlayer.position;
        ResetPoolingObject();
        LoadLevel(Pref.curPlayerLevel);
        OnInit();
    }
    

    public void RetryLevel()
    {
        ResetPoolingObject();
        player.RemoveAllBrick();
        player.transform.position = firstPosPlayer.position;
        LoadLevel(Pref.curPlayerLevel);
        OnInit();
        EnterBotPlay();
    }

    public void FinishLevel()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].StopMoving();
        }
    }
}
