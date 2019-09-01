using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    protected PlayerController pCtrl;
    protected PlayerAnimation pAni;
    protected Animator pAnimator;

    protected ePlayerAni ani;
    protected SkillData data;

    public bool isCool;
    public bool isUI;
    protected bool isEffect;

    protected MonsterContorll HitMob;

    protected float coolTime = 0.0f;
    protected float coolInterval = 1.0f;

    protected MeshRenderer renderer;

    public virtual void Init(SkillData skilldata)
    {
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
        pAni = GameManager.Instance.player.GetComponent<PlayerAnimation>();
        pAnimator = pAni.GetComponent<Animator>();

        data = skilldata;

        coolTime = 0.0f;
        coolInterval = data.coolTime;

        isCool = false;
        isUI = false;
        isEffect = false;

        ani = ePlayerAni.IDLE;
    }

    public virtual void ReadySkill()
    {
        if (isCool) { End(); return; }

        isCool = true;
        isEffect = false;

        HitMob = pCtrl.Mob;

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

    public virtual void UpdateCoolTime()
    {
        if (!isCool) return;

        coolTime += Time.deltaTime;
        if (coolTime >= coolInterval - 0.1f) 
        {
            coolTime = 0.0f;
            isCool = false;
        }
    }

    public float GetRemainTime()
    {
        return coolInterval - coolTime;
    }
}
