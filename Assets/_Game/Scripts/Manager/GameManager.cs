using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    // [SerializeField] UserData userData;
    // [SerializeField] CSVData csv;
    private static GameState gameState = GameState.MainMenu;

    // Start is called before the first frame update
    protected void Awake()
    {
        
        ChangeState(GameState.MainMenu);

        UIManager.Ins.OpenUI<MainMenu>();
    }

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
  
}
