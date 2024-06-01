using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<CharacterBrick> brickBacks = new List<CharacterBrick>();
    
    private IState<Character> currentState;
    private string currentAnim = Const.IDLE_ANIM;
    public bool isWin = false;
    public ColorType colorType;
    public Platform platform;

    public int BotBrick => brickBacks.Count;
    

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
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    protected Vector3 CheckGround(Vector3 nextPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPos, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag(Const.TAG_STAIR))
            {
                Debug.Log("vkl");
                return hit.point + Vector3.up * 1.1f;
            }
        }
        
        return TF.position;
    }
    
    protected bool IsGrounded(Vector3 nextPos)
    {
        Ray ray = new Ray(nextPos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            if (hit.collider.CompareTag(Const.TAG_GROUND))
            {
                Debug.DrawRay(nextPos, Vector3.down, Color.blue);
                Debug.Log("adu");
                return true;
            }
        }

        return false;

    }
    protected void AddBrick()
    { 
        CharacterBrick brickObject = Instantiate(brickBackPrefab, brickBack);
        brickObject.ChangeColor(colorType);
        //thay doi do cao cua gach
        brickObject.TF.localPosition = Vector3.up * 0.4f * brickBacks.Count;
        brickObject.gameObject.layer = LayerMask.NameToLayer(Const.LAYER_CHARACTER);
        brickBacks.Add(brickObject);
    }

    protected void RemoveBrick()
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

    protected internal void RemoveAllBrick()
    {
        for (int i = 0; i < brickBacks.Count; i++)
        {
            brickBacks[i].gameObject.SetActive(false);
        }
        brickBacks.Clear();
    }
    public void CheckStair()
    {
        Vector3 originPos = TF.position + Vector3.forward + Vector3.down * 1.5f;
        Ray ray = new Ray(originPos, Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(TF.position + Vector3.forward, Vector3.up, Color.red);
            if (hit.collider.CompareTag(Const.TAG_STAIR))
            {
                Debug.Log("den cau");
                //check xem con di chuyen tren cau khong
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
                LevelManager.Instance().BrickAfterChangeColor.Add(brick);
                AddBrick();
                brick.DelayAppear(brick);
            }
        }

    }
    
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        rd.material = colorData.GetColorMatByEnum((int)colorType);
    }
}
