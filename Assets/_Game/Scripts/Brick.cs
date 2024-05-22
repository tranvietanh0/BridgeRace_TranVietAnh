using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private GameObject brick;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            brick.SetActive(false);
            Debug.Log("adu");
        }
    }
}
