using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFreezing : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.FREEZING;
    }

    public override void UpdateSkill()
    {
        if (HitMob != null && !isEffect)
        {
            isEffect = true;
            HitMob.Freezing();
        }

        if (pAni.curAni == ePlayerAni.IDLE)
        {
            End();
        }
    }

    public override void End()
    {
        pCtrl.EndAttack();

        base.End();
    }
}
