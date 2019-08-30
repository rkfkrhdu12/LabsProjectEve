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

public class PlayerController : MonoBehaviour
{
    PlayerAnimation pAni;
    UIManager uiMgr;
    SkillManager skillMgr;

    public float z;
    public float y;
    public float x;

    public eSkill skillCode;
    public MonsterContorll Mob;
    public float weaponDamage;

    public bool isAttack;
    public bool isDeath;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        InitState();

        skillMgr.SetA(eSkill.FLAME);
        skillMgr.SetS(eSkill.SWIFT);
        skillMgr.SetD(eSkill.ELECTRON);
        skillMgr.SetF(eSkill.FREEZING);
    }

    void Update()
    {
        if (isDeath) return;

        UpdateInputKey();
    }

    void FixedUpdate()
    {
        if (isDeath) return;

        UpdateState();
    }

    void Init()
    {
        pAni = GetComponent<PlayerAnimation>();
        uiMgr = GetComponent<UIManager>();
        skillMgr = GetComponent<SkillManager>();

        isAttack = false;
    }

    public void SetAttack(MonsterContorll mob)
    {
        Mob = mob;
    }

    public void EndAttack()
    {
        Mob = null;
    }

    // Input
    #region Input
    void UpdateInputKey()
    {
        z = 0;
        x = 0;

        UpdateInputAttack();

        if (curState == ePlayerState.SKILL) return;

        UpdateInputUI();
        UpdateInputMove();
    }

    void UpdateInputAttack()
    {
        if (curState == ePlayerState.SKILL) return;

        if (pAni.curAni < ePlayerAni.ATTACK)
        {
            if (Input.GetKey(KeyCode.X))
            {
                skillMgr.Attack(Mob);
            }

            if (Input.GetKey(KeyCode.C))
            {
                skillCode = eSkill.SHIFT;
                curState = ePlayerState.SKILL;
            }


            if (Input.GetKey(KeyCode.A))
            {
                skillMgr.A();
            }

            if (Input.GetKey(KeyCode.S))
            {
                skillMgr.S();
            }

            if (Input.GetKey(KeyCode.D))
            {
                skillMgr.D();
            }

            if (Input.GetKey(KeyCode.F))
            {
                skillMgr.F();
            }
        }
    }

    void UpdateInputUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (uiMgr.isInventory)
            {
                curState = ePlayerState.NONE;
                uiMgr.OffInventory();
            }
            else
            {
                curState = ePlayerState.UI;
                uiMgr.OnInventory();
            }
        }
    }

    public bool isJump = false;

    void UpdateInputMove()
    {
        if (curState == ePlayerState.UI) return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            z += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            z -= 1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x -= 1;
        }

        if (x != 0) { z += .5f; }
        if (z < 0) { x *= -1; }

        z = Mathf.Clamp(z, -1f, 1);
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

        if (!isJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
                y = 1;
                curState = ePlayerState.MOVE;
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