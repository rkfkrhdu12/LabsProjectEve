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
    ATTACK01,
    ATTACK02,
}

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    private const string key_isWalk = "IsWalk";
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    private const string key_isIdle = "IsIdle";

    public ePlayerAni curAni;

    public string curAniKey;

    void Start()
    {
        curAni = ePlayerAni.IDLE;
        curAniKey = key_isIdle;

        animator = GetComponent<Animator>();

        StopAni();
    }

    void Update()
    {
        animator.SetBool(curAniKey, true);
    }

    public void StopAni()
    {
        ChangeAni(ePlayerAni.IDLE);
    }

    public void ChangeAni(ePlayerAni changeAni)
    {
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
                case ePlayerAni.ATTACK01:
                    curAniKey = key_isAttack01;
                    break;
                case ePlayerAni.ATTACK02:
                    curAniKey = key_isAttack02;
                    break;
            }
        }
    }
}