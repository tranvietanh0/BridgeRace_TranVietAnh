using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform firstBrickPos;
    [SerializeField] private GameObject brickPrefab;
    private List<GameObject> brickOnStages = new List<GameObject>();

    private Vector3 m_spawnBrickPos;

    private void Start()
    {
        EnableBrick();
    }

    private void EnableBrick()
    {
        m_spawnBrickPos = firstBrickPos.position;
        m_spawnBrickPos.y -= 0.5f;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject brickObj = Instantiate(brickPrefab, m_spawnBrickPos, brickPrefab.transform.rotation);
                brickOnStages.Add(brickObj);
                m_spawnBrickPos.x += 1.5f;
            }
            m_spawnBrickPos.z -= 2f;
            m_spawnBrickPos.x = firstBrickPos.position.x;
        }
    }
    
        
}