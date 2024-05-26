using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        if (t.isWin)
        {
            
        }
        else
        {
            
        }
    }

    public void OnExecute(Bot t)
    {
        t.CheckStair();
    }

    public void OnExit(Bot t)
    {
        throw new System.NotImplementedException();
    }
}
