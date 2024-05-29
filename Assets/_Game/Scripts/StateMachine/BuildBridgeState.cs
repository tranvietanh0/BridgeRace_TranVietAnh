using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState<Bot>
{
    private Vector3 finishPos;
    public void OnEnter(Bot t)
    {
        
        if (t.isWin)
        {
            
        }
        else
        {
            Debug.Log("len dich");
            // t.ChangeAnim(Const.RUN_ANIM);
            // bridgeStartPos = t.platform.GetBridgeStartPos();
            t.platform.brickBotTake.Clear();
            t.MoveToWinPos();
            finishPos = t.finishPos.position;
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
            // if (t.platform.isNewPlatform)
            // {
            //     Debug.Log("da vao new platform");
            //     // FindClosestBrickOnNewPlatform(t);
            //     t.ChangeState(new BuildBridgeState());
            // }
        }
    }

    public void OnExit(Bot t)
    {
        
    }

}
