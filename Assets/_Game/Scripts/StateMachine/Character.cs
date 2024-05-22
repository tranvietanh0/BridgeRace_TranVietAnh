using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform brickBack;
    [SerializeField] private GameObject brickBackPrefab;
    private List<GameObject> brickBacks = new List<GameObject>();
    private IState<Character> currentState;
    private string currentAnim;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Character> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    private void AddBrick()
    {
        GameObject brickObject = Instantiate(brickBackPrefab, brickBack);
        brickObject.transform.localPosition = Vector3.up * 0.4f * brickBacks.Count;
        Debug.Log("adu");
        brickBacks.Add(brickObject);
    }

    private void RemoveBrick()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Brick.ToString()))
        {
            AddBrick();
        }
    }
}
