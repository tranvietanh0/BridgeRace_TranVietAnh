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
    public VariableJoystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    
    private Stack<Vector3> placedBricks = new Stack<Vector3>();
    public Vector3 moveVector, nextPos;
    public bool isMoving = false;
    private bool isOnBridge = false;
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
                moveVector = Vector3.zero;
                // moveSpeed = 0;
                // rotateSpeed = 0f;
                Debug.Log("dung lai");
                isMoving = true;
            }
        }
        // quay joystick xuong thi move dc 
        if (joystick.Vertical <= 0)
        {
            // TF.position = CheckGround(nextPos);
            isMoving = false;
        }
    }
    
    private void Move()
    {
        if (!isMoving)
        {
            moveVector = Vector3.zero;
            moveVector.x = joystick.Horizontal * moveSpeed * Time.deltaTime;
            moveVector.z = joystick.Vertical * moveSpeed * Time.deltaTime;
            if (Math.Abs(joystick.Horizontal) > 0.1f || Math.Abs(joystick.Vertical) > 0.1f)
            {
                //huong quay joystick de di chuyen
                Vector3 direction =
                    Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
                //gan huong vao bien co san
                moveDirection = direction;
                ChangeAnim(Const.RUN_ANIM);

            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (!isWin)
                {
                    ChangeAnim(Const.IDLE_ANIM);
                }
                else
                {
                    RemoveAllBrick();
                    ChangeAnim(Const.DANCE_ANIM);
                }
            }
            nextPos = TF.position + moveDirection;
            transform.Translate(new Vector3(joystick.Horizontal, 0, joystick.Vertical) * moveSpeed * Time.deltaTime,
                Space.World);
            // if (IsGrounded(nextPos))
            // {
            //     Debug.Log("o mat dat");
            //     rb.MovePosition(rb.position + moveVector);
            // }
            // else
            // {
            //     transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            // }
        }
    }

    private void GetFirstPos()
    {
        transform.position = LevelManager.Instance().firstPosPlayer.position;
    }
}