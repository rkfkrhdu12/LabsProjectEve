using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerAni
{
    IDLE,
    WALK,
    RUN,
    JUMP,
    DAMAGE,
    DEAD,
    ATTACK,
    FLAME,
    SWIFT,
    ELECTRON,
    FREEZING,
}

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    PlayerController pCtrl;

    private const string key_isWalk = "IsWalk";
    private const string key_isRun = "IsRun";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    private const string key_isIdle = "IsIdle";
    private const string key_isAttack = "IsAttack";
    private const string key_isAttack1 = "IsSkill1";
    private const string key_isAttack2 = "IsSkill2";
    private const string key_isAttack3 = "IsSkill3";
    private const string key_isAttack4 = "IsSkill4";

    public ePlayerAni curAni;

    public string curAniKey;

    void Start()
    {
        curAni = ePlayerAni.IDLE;
        curAniKey = key_isIdle;
        isAttack = false;

        animator = GetComponent<Animator>();
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();

        StopAttack();
    }

    float attackTime = 0.0f;
    float attackInterval;

    void Update()
    {
        if (!isAttack) return;


    }

    bool isAttack = false;
    public void StopAttack()
    {
        isAttack = false;
        pCtrl.isAttack = false;

        ChangeAni(ePlayerAni.IDLE);
        pCtrl.ChangeState(ePlayerState.MOVE);
    }

    public void ChangeAni(ePlayerAni changeAni)
    {
        if (isAttack) return;

        Debug.Log(isAttack + " " + changeAni + " " + curAni);

        if (changeAni != curAni)
        {
            animator.SetBool(curAniKey, false);
            curAni = changeAni;

            switch (curAni)
            {
                case ePlayerAni.IDLE:
                    curAniKey = key_isIdle;
                    break;
                case ePlayerAni.WALK:
                    curAniKey = key_isWalk;
                    break;
                case ePlayerAni.RUN:
                    curAniKey = key_isRun;
                    break;
                case ePlayerAni.JUMP:
                    curAniKey = key_isJump;
                    break;
                case ePlayerAni.DAMAGE:
                    curAniKey = key_isDamage;
                    break;
                case ePlayerAni.DEAD:
                    curAniKey = key_isDead;
                    break;
                case ePlayerAni.ATTACK:
                    curAniKey = key_isAttack;
                    isAttack = true;
                    break;
                case ePlayerAni.FLAME:
                    curAniKey = key_isAttack1;
                    isAttack = true;
                    break;
                case ePlayerAni.SWIFT:
                    curAniKey = key_isAttack2;
                    isAttack = true;
                    break;
                case ePlayerAni.ELECTRON:
                    curAniKey = key_isAttack3;
                    isAttack = true;
                    break;
                case ePlayerAni.FREEZING:
                    curAniKey = key_isAttack4;
                    isAttack = true;
                    break;
            }

            animator.SetBool(curAniKey, true);
        }
    }
}