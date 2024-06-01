using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    public ColorType colorType;

    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer rd;

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        rd.material = colorData.GetColorMatByEnum((int)colorType);
    }

    public void DelayAppear(Brick brick)
    {
        if (gameObject.GetComponent<Renderer>() != null && gameObject.GetComponent<BoxCollider>() != null)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        StartCoroutine(CoDelayAppear(brick));
    }

    private IEnumerator CoDelayAppear(Brick brick)
    {
        yield return new WaitForSeconds(5f);
        if (gameObject.GetComponent<Renderer>() != null && gameObject.GetComponent<BoxCollider>() != null)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
            LevelManager.Instance().BrickAfterChangeColor.Add(brick);
        }
    }
}
