using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
}
