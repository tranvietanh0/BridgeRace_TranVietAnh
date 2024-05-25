using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform brickBack;
    [SerializeField] private CharacterBrick brickBackPrefab;
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer rd;
    [SerializeField] protected LayerMask bridgeLayer;
    [SerializeField] protected LayerMask groundLayer;
    
    protected List<CharacterBrick> brickBacks = new List<CharacterBrick>();
    public List<Vector3> emptyPos = new List<Vector3>();
    private IState<Character> currentState;
    private string currentAnim = Const.IDLE_ANIM;
    protected bool isMoveOnStair = true;
    public bool isWin = false;
    public ColorType colorType;
    

    // Update is called once per frame
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public virtual void OnInit()
    {
        RemoveAllBrick();
    }

    public void ChangeAnim(string animName)
    {
        Debug.Log(animName);
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    private void AddBrick()
    { 
        CharacterBrick brickObject = Instantiate(brickBackPrefab, brickBack);
        brickObject.ChangeColor(colorType);
        //thay doi do cao cua gach
        brickObject.transform.localPosition = Vector3.up * 0.4f * brickBacks.Count;
        brickBacks.Add(brickObject);
    }

    private void RemoveBrick()
    {
        Vector3 originPos = TF.position + Vector3.forward + Vector3.down * 1.5f;
        Ray ray = new Ray(originPos, Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(TF.position + Vector3.forward, Vector3.up, Color.red);
            if (hit.collider.CompareTag(Const.TAG_STAIR))
            {
                CharacterBrick brickBackPlayer = brickBacks[brickBacks.Count - 1];
                brickBacks.RemoveAt(brickBacks.Count - 1);
                Destroy(brickBackPlayer.gameObject);
            }
        }
    }

    private void RemoveAllBrick()
    {
        for (int i = 0; i < brickBacks.Count; i++)
        {
            brickBacks.Clear();
        }
    }
    protected void CheckStair()
    {
        Vector3 originPos = TF.position + Vector3.forward + Vector3.down * 1.5f;
        Ray ray = new Ray(originPos, Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(TF.position + Vector3.forward, Vector3.up, Color.red);
            if (hit.collider.CompareTag(Const.TAG_STAIR))
            {
                //check xem con di chuyen tren cau khong
                isMoveOnStair = true;
                //goi collider cua stair tu cache
                Stair stair = Cache.GetStair(hit.collider);
                if (stair.colorType != colorType)
                {
                    stair.ChangeColor(colorType);
                    RemoveBrick();
                }
            }
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if (brick.colorType == colorType)
            {
                brick.DelayAppear();
                AddBrick();
            }
        }

    }
    
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        rd.material = colorData.GetColorMatByEnum((int)colorType);
    }
}
