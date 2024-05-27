using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBrickState : IState<Bot>
{
    private int brickOfBackBot = 4;
    private bool isBrickCollected = false;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Const.RUN_ANIM);
        FindClosestBrick(t);
    }

    public void OnExecute(Bot t)
    {
        if (t.agent.remainingDistance < t.agent.stoppingDistance && !t.agent.pathPending)
        {
            if (isBrickCollected)
            {
                isBrickCollected = false;
                FindClosestBrick(t); // Tìm viên gạch gần nhất tiếp theo sau khi thu thập gạch
            }

            if (t.BotBrick >= brickOfBackBot)
            {
                t.ChangeState(new BuildBridgeState());
            }
        }
    }

    public void OnExit(Bot t)
    {
        // Đặt lại biến khi thoát trạng thái
        isBrickCollected = false;
    }

    public void FindClosestBrick(Bot t)
    {
        Brick sameColorBrick = t.platform.FindSameColor(t.colorType);
        if (sameColorBrick != null)
        {
            t.agent.SetDestination(sameColorBrick.TF.position);
            isBrickCollected = true;
        }
        else
        {
            Debug.Log("k tim thay gach");
        }
    }
}
