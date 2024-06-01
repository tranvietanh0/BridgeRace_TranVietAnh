using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPos : MonoBehaviour
{
    [SerializeField] private Character player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Const.TAG_PLAYER))
        {
            Debug.Log("Win");
            player.isWin = true;
            other.gameObject.GetComponent<Character>().ChangeAnim(Const.DANCE_ANIM);
            DelayChangeLevel();
        }
    }

    IEnumerator DelayChangeLevel()
    {
        yield return new WaitForSeconds(3f);
        LevelManager.Instance().NextLevel();
    }
}
