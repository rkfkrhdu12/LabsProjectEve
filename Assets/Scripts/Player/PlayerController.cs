using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State
public enum ePlayerState
{
    NONE,
    MOVE,
    SKILL,
    UI,
    LAST,
}

public class PlayerController : Character
{
    private void Awake()
    {
        Init();

        InitState();
    }

    void FixedUpdate()
    {
        UpdateInputKey();
        UpdateState();
    }

    // Input
    #region Input
    void UpdateInputKey()
    {
        UpdateInputAttack();
        UpdateInputUI();
        UpdateInputMove();
    }

    void UpdateInputAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            skillCode = eSkill.DEFAULT_ATTACK;
            curState = ePlayerState.SKILL;
            stateMgr.state[(int)curState].ReadyState();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            skillCode = eSkill.TELEPORT;
            curState = ePlayerState.SKILL;
            stateMgr.state[(int)curState].ReadyState();
        }
    }

    void UpdateInputUI()
    {
        if (curState != ePlayerState.SKILL)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {

            }
        }
    }

    void UpdateInputMove()
    {
        z = 0;
        x = 0;

        if (curState != ePlayerState.SKILL && curState != ePlayerState.UI)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                z += 1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                z -= 1;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                z += 1 * .3f;
                x += 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                z += 1 * .3f;
                x -= 1;
            }
            z = Mathf.Clamp(z, -1, 1);
            x = Mathf.Clamp(x, -1, 1);

            if (z != 0)
            {
                curState = ePlayerState.MOVE;
                stateMgr.state[(int)curState].ReadyState();

                pAni.ChangeAni(ePlayerAni.RUN);
            }
            else
            {
                curState = ePlayerState.NONE;
                pAni.ChangeAni(ePlayerAni.IDLE);
            }
        }
    }
    #endregion

    // State
    #region State
    ePlayerState curState;
    StateManager stateMgr;

    void InitState()
    {
        curState = ePlayerState.MOVE;
        stateMgr = GetComponent<StateManager>();
    }

    void UpdateState()
    {
        stateMgr.state[(int)curState].UpdateState();
    }

    public void ChangeState(ePlayerState state)
    {
        curState = state;
    }
    #endregion

    // Move
    public float moveSpeed = 10;
    public float rotateSpeed = 60;

    public float z;
    public float x;

    // Skill
    public eSkill skillCode;

    // Animation
    #region Animation
    public PlayerAnimation pAni;

    #endregion

    public GameObject mesh;

    void Init()
    {
        mesh = transform.GetChild(0).gameObject;
        health = 100;
    }
}