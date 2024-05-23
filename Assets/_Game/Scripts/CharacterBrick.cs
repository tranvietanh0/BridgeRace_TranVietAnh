using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrick : GameUnit
{
    public ColorType colorType;

    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer renderer;

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorData.GetColorMatByEnum((int)colorType);
    }
}
