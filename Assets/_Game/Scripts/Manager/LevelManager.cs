using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GOSingleton<LevelManager>
{
    [SerializeField] public Transform firstPosPlayer;
    public Player player;
    public Bot bot;
    public Platform platform;

    private List<ColorType> colorTypes = new List<ColorType>() {ColorType.Red, ColorType.Green, ColorType.Yellow, ColorType.Orange, ColorType.Purple, ColorType.Black };

    public List<ColorType> colorRandoms = new List<ColorType>();

    private List<Bot> bots = new List<Bot>();
    
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnInit()
    {
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
        for (int i = 0; i < 45; i++)
        {
            int randomColorIndex = Random.Range(0, colorRandoms.Count);
            platform.EnableBrick();
            platform.ChangeColorBrickOnStage(colorRandoms);
            // platform.TakeColor(colorRandoms[randomColorIndex]);
        }
        //set mau cho player
        int randomIndexPlayerColor = Random.Range(0, colorRandoms.Count);
        player.ChangeColor(colorRandoms[randomIndexPlayerColor]);
        colorRandoms.RemoveAt(randomIndexPlayerColor);

        for (int i = 0; i < 3; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, characterPos[i], Quaternion.identity);
            bot.ChangeColor(colorRandoms[i]);
        }
    }
}
