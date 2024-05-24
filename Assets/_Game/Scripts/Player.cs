using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    private Stack<Vector3> placedBricks = new Stack<Vector3>();
    private Vector3 m_moveVector;
    private bool isMoving = false;
    private Vector3 moveDirection;


    private void Start()
    {
        GetFirstPos();
    }
    private void Update()
    {
        Move();
        //to mau cho gach
        if (brickBacks.Count > 0)
        {
            CheckStair();
        }

        if (brickBacks.Count == 0 && Physics.Raycast(TF.position + Vector3.forward + Vector3.down * 1.5f, Vector3.up, out RaycastHit hit))
        {
            Stair stair = Cache.GetStair(hit.collider);
            // check stair va mau khac vs mau player thi moi dung lai
            if (hit.collider.CompareTag(Const.TAG_STAIR) && stair.colorType != colorType)
            {
                Debug.DrawRay(TF.position, Vector3.down, Color.green, bridgeLayer);
                m_moveVector = Vector3.zero;
                // moveSpeed = 0;
                // rotateSpeed = 0f;
                Debug.Log("dung lai");
                isMoving = true;
            }
        }
        // quay joystick xuong thi move dc 
        if (joystick.Vertical <= 0)
        {
            isMoving = false;
        }
    }
    
    private void Move()
    {
        if (!isMoving)
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

            // transform.Translate(new Vector3(joystick.Horizontal, 0, joystick.Vertical) * moveSpeed * Time.deltaTime, Space.World);
            rb.MovePosition(rb.position + m_moveVector);
        }
    }

    private void GetFirstPos()
    {
        transform.position = LevelManager.Instance().firstPosPlayer.position;
    }
}