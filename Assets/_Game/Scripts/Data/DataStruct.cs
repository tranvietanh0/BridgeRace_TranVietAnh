using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    None = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
    Orange = 4,
    Purple = 5,
    Black = 6
}

public enum GameState
{
    None = 0,
    MainMenu = 1,
    GamePlay = 2,
    Pause = 3
}

public enum BotState
{
    None = 0,
    Idle = 1,
    TakeBrick = 2,
    BuildBridge = 3
}


public class Const
{
    public const string DANCE_ANIM = "dance";
    public const string IDLE_ANIM = "idle";
    public const string RUN_ANIM = "run";

    public const string TAG_BRICK = "Brick";
    public const string TAG_PLAYER = "Player";
    public const string TAG_STAIR = "Floor";
    public const string TAG_WINPOS = "Win";
    public const string TAG_GROUND = "Ground";
    
    public const string LAYER_CHARACTER = "Character";
    public const string LAYER_BRIDGE = "Bridge";
    public const string LAYER_GROUND = "Ground";
    

}