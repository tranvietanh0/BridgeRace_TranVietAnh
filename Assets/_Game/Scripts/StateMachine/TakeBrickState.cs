using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBrickState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Const.RUN_ANIM);
    }

    public void OnExecute(Bot t)
    {
        FindClosestBrick(t);
    }

    public void OnExit(Bot t)
    {
    }

    public void FindClosestBrick(Bot t)
    {
        Brick sameColorBrick = t.platform.FindSameColor(t.colorType);
        if (sameColorBrick == null)
        {
            t.ChangeState(new IdleState());
        }
        else
        {
            t.agent.SetDestination(sameColorBrick.TF.position);
        }
    }
}
