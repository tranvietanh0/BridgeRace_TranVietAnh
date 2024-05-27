using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState<Bot>
{
    private Vector3 bridgeStartPos;
    public void OnEnter(Bot t)
    {
        if (t.isWin)
        {
            
        }
        else
        {
            t.ChangeAnim(Const.RUN_ANIM);
            // bridgeStartPos = t.platform.GetBridgeStartPos();
            t.MoveToNextPlatform();
        }
    }

    public void OnExecute(Bot t)
    {
        if (t.BotBrick == 0)
        {
            t.ChangeState(new TakeBrickState());
        }
        else
        {
            if (t.agent.remainingDistance < t.agent.stoppingDistance && !t.agent.pathPending)
            {
                Debug.Log("kkkk");
                t.MoveToNextPlatform();
                t.CheckStair();
            }
        }
    }

    public void OnExit(Bot t)
    {
        
    }
}
