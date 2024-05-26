using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Const.IDLE_ANIM);
    }

    public void OnExecute(Bot t)
    {
        t.ChangeState(new TakeBrickState());
        
    }

    public void OnExit(Bot t)
    {
        
    }
}
