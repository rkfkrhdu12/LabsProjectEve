using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    protected PlayerController pCtrl;
    protected PlayerAnimation pAni;
    protected Animator pAnimator;

    protected ePlayerAni ani;

    public bool isCool;
    public bool isUI;
    public int UINumber;

    protected MonsterContorll HitMob;
    protected float weaponDamage;

    protected float coolTime = 0.0f;
    protected float coolInterval = 1.0f;

    public virtual void Init(float cool)
    {
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
        pAni = pCtrl.mesh.GetComponent<PlayerAnimation>();
        pAnimator = pAni.GetComponent<Animator>();

        coolTime = 0.0f;
        coolInterval = cool;
        isCool = false;
        isUI = false;
        ani = ePlayerAni.IDLE;
    }

    public virtual void ReadySkill()
    {
        if (isCool) { End(); return; }

        isCool = true;
        pCtrl.ChangeState(ePlayerState.SKILL);
        if (ePlayerAni.ATTACK <= ani)
        {
            pAni.ChangeAni(ani);
        }
    }

    public virtual void UpdateSkill()
    {
        End();
    }

    public virtual void End()
    {
    }

    public bool IsCool()
    {
        return isCool;
    }

    public virtual void SetAttack(MonsterContorll mob,float weapondamage)
    {
        HitMob = mob;
        weaponDamage = weapondamage;
    }

    public virtual void UpdateCoolTime()
    {
        if (!isCool) return;

        coolTime += Time.deltaTime;
        if (coolTime >= coolInterval)
        {
            coolTime = 0.0f;
            isCool = false;
        }
    }
}
