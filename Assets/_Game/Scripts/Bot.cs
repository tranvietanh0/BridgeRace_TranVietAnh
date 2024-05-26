using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Transform finishPos;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void MoveToWinPos()
    {
        agent.SetDestination(finishPos.position);
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(Const.IDLE_ANIM);
    }

    public void ChangeState(IState<Bot> state)
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
}
