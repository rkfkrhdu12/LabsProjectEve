using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFreezing : Skill
{
    public override void Init(SkillData skilldata)
    {
        base.Init(skilldata);

        ani = ePlayerAni.FREEZING;
    }

    public override void ReadySkill()
    {
        base.ReadySkill();

        pCtrl.isAttack = true;
    }
    public override void UpdateSkill()
    {
        if (HitMob != null && !isEffect)
        {
            isEffect = true;

            HitMob.GetDamage(pCtrl.weaponDamage * data.damage);
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
