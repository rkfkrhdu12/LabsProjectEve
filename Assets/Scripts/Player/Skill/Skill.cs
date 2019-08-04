using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSkill
{
    NONE,
    DEFAULT_ATTACK,
    TELEPORT,
    LAST,
}

public class Skill
{
    protected PlayerController pCtrl;
    protected PlayerAnimation pAni;
    protected Animator pAnimator;
    protected bool isCool;
    protected float coolTime = 0.0f;
    protected float coolInterval = 1.0f;

    public virtual void Init()
    {
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
        pAni = pCtrl.mesh.GetComponent<PlayerAnimation>();
        pAnimator = pAni.GetComponent<Animator>();

        coolTime = 0.0f;
        coolInterval = 0.0f;
    }

    public virtual void ReadySkill()
    {
        pCtrl.ChangeState(ePlayerState.SKILL);
    }

    public virtual void UpdateSkill()
    {
        End();
    }

    public virtual void End()
    {
        pCtrl.ChangeState(ePlayerState.MOVE);
    }

    public bool IsCool()
    {
        return isCool;
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
