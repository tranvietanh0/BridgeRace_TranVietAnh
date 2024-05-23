using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]

public class ColorData : ScriptableObject
{
    public List<Material> colorMats;

    public Material GetColorMatByEnum(int index)
    {
        for (int i = 0; i < colorMats.Count; i++)
        {
            if (i == index)
            {
                return colorMats[i];
            }
        }

        return colorMats[0];
    }
}
