using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public void Close()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    public void ContinueButton()
    {
        Close();
    }

    public void RetryButton()
    {
        LevelManager.Instance().RetryLevel();
        Close();
    }
}
