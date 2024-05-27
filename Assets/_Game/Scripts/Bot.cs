
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Transform finishPos;
    [SerializeField] private List<GameObject> nextPlatform = new List<GameObject>();

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

    public void MoveToNextPlatform()
    {
        float minDistance = Mathf.Infinity;
        Vector3 tmpPos;
        for (int i = 0; i < nextPlatform.Count; i++)
        {
            float distance = Vector3.Distance(nextPlatform[i].transform.position, this.TF.position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        for (int i = 0; i < nextPlatform.Count; i++)
        {
            float distance = Vector3.Distance(nextPlatform[i].transform.position, this.TF.position);
            if (Mathf.Abs(distance - minDistance) < 0.1f)
            {
                agent.SetDestination(nextPlatform[i].transform.position);
            }
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
