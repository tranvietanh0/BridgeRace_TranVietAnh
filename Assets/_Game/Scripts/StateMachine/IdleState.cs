using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float randomTime;
    private float timer;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Const.IDLE_ANIM);
        timer = 0f;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Bot t)
    {
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            t.ChangeState(new TakeBrickState());
        }
    }

    public void OnExit(Bot t)
    {
        
    }
}
