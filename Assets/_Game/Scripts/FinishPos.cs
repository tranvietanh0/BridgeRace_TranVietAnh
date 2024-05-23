using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Const.TAG_PLAYER))
        {
            Debug.Log("Win");
            other.gameObject.GetComponent<Character>().ChangeAnim(Const.DANCE_ANIM);
        }
    }
}
