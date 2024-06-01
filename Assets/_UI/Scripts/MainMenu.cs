using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        // LevelManager.Instance().LoadLevel(Pref.curPlayerLevel);
        // LevelManager.Instance().OnInit();
        LevelManager.Instance().EnterBotPlay();
        Close(0);
    }
}
