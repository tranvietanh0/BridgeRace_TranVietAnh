using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : GameUnit
{
    public ColorType colorType;

    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer rd;

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        rd.material = colorData.GetColorMatByEnum((int)colorType);
    }
}
