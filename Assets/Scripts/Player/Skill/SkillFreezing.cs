using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFreezing : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.FREEZING;
        UINumber = 3;
    }

    public override void ReadySkill()
    {
        base.ReadySkill();

        pAnimator.speed = pCtrl.aniSpeed;
        if (HitMob != null)
            HitMob.Freezing();
    }

    public override void UpdateSkill()
    {
        if (pAni.curAni == ePlayerAni.IDLE)
        {
            End();
        }
    }

    public override void End()
    {
        pCtrl.EndAttack();
        pAni.ChangeAni(ePlayerAni.IDLE);

        base.End();
    }
}
