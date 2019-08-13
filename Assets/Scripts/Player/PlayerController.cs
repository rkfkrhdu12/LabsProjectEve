﻿using System.Collections;
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

public class PlayerController : MonoBehaviour
{
    public PlayerAnimation pAni;
    public UI ui;

    public float moveSpeed = 7;
    public float rotateSpeed = 80;

    public float z;
    public float x;

    public eSkill skillCode;

    public GameObject mesh;


    private void Awake()
    {
        Init();

        InitState();
    }

    void Update()
    {
        UpdateInputKey();
    }

    void FixedUpdate()
    {
        UpdateState();
    }

    void Init()
    {
        mesh = gameObject;

        ui = GetComponent<UI>();
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
        if (curState != ePlayerState.SKILL)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                skillCode = eSkill.DEFAULT_ATTACK;
                curState = ePlayerState.SKILL;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                skillCode = eSkill.TELEPORT;
                curState = ePlayerState.SKILL;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                return;
                //skillMgr.A();
                //curState = ePlayerState.SKILL;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                return;
                //skillMgr.S();
                //curState = ePlayerState.SKILL;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                return;
                //skillMgr.D();
                //curState = ePlayerState.SKILL;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                return;
                //skillMgr.F();
                //curState = ePlayerState.SKILL;
            }
        }
    }

    void UpdateInputUI()
    {
        if (curState != ePlayerState.SKILL)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (ui.isInventory)
                {
                    curState = ePlayerState.NONE;
                    ui.OffInventory();
                }
                else
                {
                    curState = ePlayerState.UI;
                    ui.OnInventory();
                }
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
                z += .5f;
                x += 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                z += .5f;
                x -= 1;
            }
            z = Mathf.Clamp(z, -1, 1);
            x = Mathf.Clamp(x, -1, 1);

            if (z != 0)
            {
                curState = ePlayerState.MOVE;

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
    public ePlayerState s;
    public ePlayerState curState
    {
        get
        {
            return s;
        }
        set
        {
            if (s != value)
            {
                s = value;
                if (s != ePlayerState.NONE)
                    stateMgr.state[(int)s].ReadyState();
            }
        }
    }
    StateManager stateMgr;

    void InitState()
    {
        stateMgr = GetComponent<StateManager>();
        curState = ePlayerState.MOVE;
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

}