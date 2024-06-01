using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState<Bot>
{
    private Vector3 finishPos;
    public void OnEnter(Bot t)
    {
        
        if (t.isBotWin)
        {
            
        }
        else
        {
            // t.ChangeAnim(Const.RUN_ANIM);
            // bridgeStartPos = t.platform.GetBridgeStartPos();
            t.platform.brickBotTake.Clear();
            t.MoveToWinPos();
            finishPos = t.finishPos.position;
            Debug.Log("adu" + finishPos);
        }
    }

    public void OnExecute(Bot t)
    {
        Debug.Log("excute cua buildbridge");
        if (t.BotBrick == 0)
        {
            Debug.Log("chuyen ve state cu");
            t.ChangeState(new TakeBrickState());
        }
        else
        {
            t.CheckMoveOnBridge();
            t.MoveToWinPos();
            Debug.Log("else cua excute bui bo rit");
            
        }
    }

    public void OnExit(Bot t)
    {
        
    }

}
