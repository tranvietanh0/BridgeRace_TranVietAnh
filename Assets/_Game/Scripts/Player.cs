using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

//
// [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    private Vector3 m_moveVector;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        m_moveVector = Vector3.zero;
        m_moveVector.x = joystick.Horizontal * moveSpeed * Time.deltaTime;
        m_moveVector.z = joystick.Vertical * moveSpeed * Time.deltaTime;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction =
                Vector3.RotateTowards(transform.forward, m_moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            ChangeAnim(Const.RUN_ANIM);
            
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            ChangeAnim(Const.IDLE_ANIM);
        }
        rb.MovePosition(rb.position + m_moveVector);
    }
}