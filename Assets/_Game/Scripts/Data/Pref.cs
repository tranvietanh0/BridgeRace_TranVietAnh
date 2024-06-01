using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref
{
    public static int curPlayerLevel
    {
        set => PlayerPrefs.SetInt(GamePref.CurLevelId.ToString(), value);
        get => PlayerPrefs.GetInt(GamePref.CurLevelId.ToString(), 0);
    }
}
