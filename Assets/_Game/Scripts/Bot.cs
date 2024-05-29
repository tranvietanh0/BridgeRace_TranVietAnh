
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform finishPos;
    

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

    

    public void CheckMoveOnBridge()
    {
        Debug.Log(BotBrick);
        if (BotBrick > 0)
        {
            CheckStair();
        }

        else
        {
            if (Physics.Raycast(TF.position + Vector3.forward + Vector3.down * 1.5f, Vector3.up, out RaycastHit hit))
            {
                Stair stair = Cache.GetStair(hit.collider);
                // check stair va mau khac vs mau player thi moi dung lai
                if (hit.collider.CompareTag(Const.TAG_STAIR) && stair.colorType != colorType)
                {
                    Debug.DrawRay(TF.position, Vector3.down, Color.green, bridgeLayer);
                    agent.speed = 0f;
                    // moveSpeed = 0;
                    // rotateSpeed = 0f;
                    Debug.Log("dung lai");
                }
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
