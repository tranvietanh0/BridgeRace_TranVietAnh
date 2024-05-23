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
    private List<CharacterBrick> brickBacks = new List<CharacterBrick>();
    private IState<Character> currentState;
    private string currentAnim;
    public ColorType colorType;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Character> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
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

    private void AddBrick()
    { 
        CharacterBrick brickObject = Instantiate(brickBackPrefab, brickBack);
        brickObject.ChangeColor(colorType);
        brickObject.transform.localPosition = Vector3.up * 0.4f * brickBacks.Count;
        brickBacks.Add(brickObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if (brick.colorType == colorType)
            {
                Destroy(brick.gameObject);
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
