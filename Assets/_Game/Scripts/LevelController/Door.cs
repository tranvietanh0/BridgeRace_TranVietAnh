
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Platform platform;
    private List<ColorType> m_colorTypes = new List<ColorType>();

    private void Update()
    {
        CloseDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null && !m_colorTypes.Contains(character.colorType) )
        {
            m_colorTypes.Add(character.colorType);
            character.platform = platform;
            // platform.brickBotTake.Clear();
            character.platform.OnEmptyPoint();
            platform.BrickOnNextPlatform(character);
        }
    }

    private void CloseDoor()
    {
        Ray ray = new Ray(transform.position + Vector3.right * 10f, Vector3.left);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position + Vector3.right * 5f, Vector3.left, Color.red);
            Character character = Cache.GetCharacter(hit.collider);
            if (hit.collider.CompareTag(Const.TAG_PLAYER) && character is Player)
            {
                Player player = character.GetComponent<Player>();
                if (player.joystick.Vertical <= 0)
                {
                    player.moveVector = Vector3.zero;
                    player.isMoving = true;
                }
                else
                {
                    player.isMoving = false;
                }
            }
        }
    }
}
