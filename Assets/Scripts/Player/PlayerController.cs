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
    public float x;

    public eSkill skillCode;
    MonsterContorll Mob;
    public float weaponDamage;

    public bool isAttack;

    private void Awake()
    {
        Init();

        InitState();
    }

    private void Start()
    {
        skillMgr.SetA(eSkill.FLAME);
        skillMgr.SetS(eSkill.SWIFT);
        skillMgr.SetD(eSkill.ELECTRON);
        skillMgr.SetF(eSkill.FREEZING);
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
        pAni = GetComponent<PlayerAnimation>();
        uiMgr = GetComponent<UIManager>();
        skillMgr = GetComponent<SkillManager>();

        isAttack = false;
    }

    public void SetAttack(MonsterContorll mob,float weapondamage)
    {
        Mob = mob;
        weaponDamage = weapondamage;
    }

    public void EndAttack()
    {
        Mob = null;
        weaponDamage = 0;
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
        if (curState != ePlayerState.SKILL && pAni.curAni < ePlayerAni.ATTACK)
        {
            if (Input.GetKey(KeyCode.X))
            {
                skillCode = eSkill.DEFAULT_ATTACK;
                curState = ePlayerState.SKILL;
                isAttack = true;
            }

            if (Input.GetKey(KeyCode.C))
            {
                skillCode = eSkill.SHIFT;
                curState = ePlayerState.SKILL;
            }


            if (Input.GetKey(KeyCode.A))
            {
                skillMgr.A(Mob,weaponDamage);
                isAttack = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                skillMgr.S(Mob, weaponDamage);
                isAttack = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                skillMgr.D(Mob, weaponDamage);
                isAttack = true;
            }

            if (Input.GetKey(KeyCode.F))
            {
                skillMgr.F(Mob, weaponDamage);
                isAttack = true;
            }
        }
    }

    void UpdateInputUI()
    {
        if (curState != ePlayerState.SKILL)
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

            if (x != 0)  { z += .5f; }
            if (z < 0) { x *= -1; }

            z = Mathf.Clamp(z, -.5f, 1);
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