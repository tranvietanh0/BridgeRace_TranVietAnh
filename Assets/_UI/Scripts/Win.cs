using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public void Retry()
    {
        LevelManager.Instance().RetryLevel();
        Close(0);
    }

    public void Next()
    {
        LevelManager.Instance().NextLevel();
        Close(0);
    }
}
