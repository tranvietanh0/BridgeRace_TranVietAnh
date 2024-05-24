
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Platform platform;
    private List<ColorType> m_colorTypes = new List<ColorType>();

    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (!m_colorTypes.Contains(character.colorType))
        {
            m_colorTypes.Add(character.colorType);
        }
        platform.OnEmptyPoint();
        platform.BrickOnNextPlatform(character);
    }
}
