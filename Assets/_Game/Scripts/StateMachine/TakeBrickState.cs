using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBrickState : IState<Bot>
{
    private int brickOfBackBot = 5;
    private bool isBrickCollected = false;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Const.RUN_ANIM);
        FindClosestBrick(t);
    }

    public void OnExecute(Bot t)
    {
        Debug.Log("excute cua take brick state");
        Debug.Log("so gach botbrick ne");
        if (t.agent.remainingDistance < t.agent.stoppingDistance)
        {
            // if (isBrickCollected)
            // {
            //     isBrickCollected = false;
            //     FindClosestBrick(t); // Tìm viên gạch gần nhất tiếp theo sau khi thu thập gạch
            // }

            if (t.BotBrick >= brickOfBackBot)
            {
                Debug.Log("di xay cau");
                t.ChangeState(new BuildBridgeState());
            }
            else
            {
                t.ChangeState(new TakeBrickState());
                // FindClosestBrick(t);
            }
        }
    }

    public void OnExit(Bot t)
    {
        // Đặt lại biến khi thoát trạng thái
        // isBrickCollected = false;
    }

    public void FindClosestBrick(Bot t)
    {
        Debug.Log(t.platform);
        Brick sameColorBrick = t.platform.FindSameColor(t.colorType, t);
        if (sameColorBrick != null)
        {
            t.agent.SetDestination(sameColorBrick.TF.position);
            isBrickCollected = true;
        }
        else
        {
            Debug.Log("ko co gach");
            t.ChangeState(new BuildBridgeState());
        }
    }
}
