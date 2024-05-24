using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] public Transform firstBrickPos;
    [SerializeField] public Brick brickPrefab;
    [SerializeField] public List<Vector3> brickPositions = new List<Vector3>();
    [SerializeField] private Character character;
    
    public List<Brick> brickOnStages = new List<Brick>();
    public List<Brick> brickAfterChangeColor = new List<Brick>();
    public List<Vector3> brickNewPlatforms = new List<Vector3>();
    public List<Vector3> checkPos = new List<Vector3>();
    public ColorType colorType;
    
    public Vector3 spawnBrickPos;
    

    // sinh ra cac vi tri rong de xep gach vao moi stage
    public void OnEmptyPoint()
    {
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
        int sumRandomBrick = brickPositions.Count / 3;
        while (sumRandomBrick != 0)
        {
            
            int randomIndex = Random.Range(0, sumRandomBrick);
            if (!brickNewPlatforms.Contains(brickPositions[randomIndex]))
            {
                brickNewPlatforms.Add(brickPositions[randomIndex]);
                Brick brick = SimplePool.Spawn<Brick>(brickPrefab, brickPositions[randomIndex], Quaternion.identity);
                brick.ChangeColor(character.colorType);
            }
            sumRandomBrick--;
        }
    }

    // private void SpawnBrickAfterTake(List<ColorType> colorTypes)
    // {
    //     if (brickOnStages.Count < 40)
    //     {
    //         for (int i = 0; i < character.emptyPos.Count; i++)
    //         {
    //             Brick brick = SimplePool.Spawn<Brick>(brickPrefab, character.emptyPos[i], Quaternion.identity);
    //             int colorIndex = i % colorTypes.Count;
    //             brick.ChangeColor(colorTypes[colorIndex]);
    //         }
    //     }
    // }

    public void FindSameColor()
    {
        
    }
        
}