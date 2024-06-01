using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPos : MonoBehaviour
{
    [SerializeField] private Character player;
    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null)
        {
            LevelManager.Instance().FinishLevel();
            if (character is Player)
            {
                character.isPlayerWin = true;
                UIManager.Ins.OpenUI<Win>();
            }
            else
            {
                UIManager.Ins.OpenUI<Lose>();
            }
        }
    }

    IEnumerator DelayChangeLevel()
    {
        yield return new WaitForSeconds(3f);
        LevelManager.Instance().NextLevel();
    }
}
