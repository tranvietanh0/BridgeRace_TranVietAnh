using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    public Transform firstBrickPos;
    public Brick brickPrefab;
    public List<Vector3> brickPositions = new List<Vector3>();
    [SerializeField] private Character character;
    [SerializeField] private List<Transform> bridgeStartPos = new List<Transform>();
    
    
    public List<Brick> brickOfNewPlatform = new List<Brick>();
    public List<Brick> brickOnStages = new List<Brick>();
    public List<Brick> brickAfterChangeColor = new List<Brick>();
    public List<Vector3> brickNewPlatforms = new List<Vector3>();
    public List<Brick> brickBotTake = new List<Brick>();
    public List<Vector3> checkPos = new List<Vector3>();
    
    public ColorType colorType;
    public Vector3 spawnBrickPos;
    public bool isNewPlatform = false;
    public int currentPlatformIndex = 0;
    

    // public void OnInit()
    // {
    //     OnEmptyPoint();
    // }
    
    // sinh ra cac vi tri rong de xep gach vao moi stage
    public void OnEmptyPoint()
    {
        brickPositions.Clear();
        spawnBrickPos = firstBrickPos.position;
        spawnBrickPos.y -= 0.5f;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                brickPositions.Add(spawnBrickPos);
                spawnBrickPos.x += 1.5f;
            }
            spawnBrickPos.z -= 2f;
            spawnBrickPos.x = firstBrickPos.position.x;
        }
    }
    public void ClearBrickPositions()
    {
        brickPositions.Clear();
        checkPos.Clear();
        brickOnStages.Clear();
        brickAfterChangeColor.Clear();
        brickBotTake.Clear();
    }

    public void EnableBrick()
    {
        while (checkPos.Count < brickPositions.Count)
        {
            int randomIndex = Random.Range(0, brickPositions.Count);
            if (!checkPos.Contains(brickPositions[randomIndex]))
            {
                checkPos.Add(brickPositions[randomIndex]);
                Brick brick = SimplePool.Spawn<Brick>(brickPrefab, brickPositions[randomIndex], Quaternion.identity);
                brickOnStages.Add(brick);
            }
        }
    }

    public void ChangeColorBrickOnStage(List<ColorType> colorTypes)
    {
        // brickAfterChangeColor.Clear();
        for (int i = 0; i < brickOnStages.Count; i++)
        {
            int colorIndex = i % colorTypes.Count;
            if (!brickAfterChangeColor.Contains(brickOnStages[i]))
            {
                brickOnStages[i].ChangeColor(colorTypes[colorIndex]);
                brickAfterChangeColor.Add(brickOnStages[i]);
            }
        }
    }

    public void BrickOnNextPlatform(Character character)
    {
        isNewPlatform = true;
        int sumRandomBrick = brickPositions.Count / 4;
        while (sumRandomBrick != 0)
        {
            int randomIndex = Random.Range(0, brickPositions.Count);
            if (!brickNewPlatforms.Contains(brickPositions[randomIndex]))
            {
                brickNewPlatforms.Add(brickPositions[randomIndex]);
                Brick brick = SimplePool.Spawn<Brick>(brickPrefab, brickPositions[randomIndex], Quaternion.identity);
                brick.ChangeColor(character.colorType);
                brickAfterChangeColor.Add(brick);
                LevelManager.Instance().BrickAfterChangeColor.Add(brick);
                Debug.Log("da them gach");
            }
            sumRandomBrick--;
        }
    }
    
    public Brick FindSameColor(ColorType colorType, Bot t) 
    {
        Brick closestBrick = null;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < LevelManager.Instance().BrickAfterChangeColor.Count; i++)
        {
            Brick brick = LevelManager.Instance().BrickAfterChangeColor[i];
            if (brick.colorType == colorType && !brickBotTake.Contains(brick))
            {
                float distance = Vector3.Distance(t.TF.position, brick.TF.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestBrick = brick;
                }
            }
        }
    
        if (closestBrick != null)
        {
            brickBotTake.Add(closestBrick);
        }
    
        return closestBrick;
    }
    // public Brick FindSameColor(ColorType colorType)
    // {
    //     Brick brick = null;
    //     for (int i = 0; i < LevelManager.Instance().BrickAfterChangeColor.Count; i++)
    //     {
    //         if (LevelManager.Instance().BrickAfterChangeColor[i].colorType == colorType)
    //         {
    //             brick = LevelManager.Instance().BrickAfterChangeColor[i];
    //         }
    //     }
    //
    //     return brick;
    // }
    
    
        
}