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
    public List<Brick> brickOnStages = new List<Brick>();
    public List<Brick> brickAfterChangeColor = new List<Brick>();
    public List<Vector3> checkPos = new List<Vector3>();
    public ColorType colorType;

    public Vector3 m_spawnBrickPos;

    private void Start()
    {
    }

    public void OnEmptyPoint()
    {
        m_spawnBrickPos = firstBrickPos.position;
        m_spawnBrickPos.y -= 0.5f;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                brickPositions.Add(m_spawnBrickPos);
                m_spawnBrickPos.x += 1.5f;
            }
            m_spawnBrickPos.z -= 2f;
            m_spawnBrickPos.x = firstBrickPos.position.x;
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

    public void FindSameColor()
    {
        
    }
        
}