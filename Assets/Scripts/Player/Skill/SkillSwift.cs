using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwift : Skill
{
    public override void Init(SkillData skilldata)
    {
        base.Init(skilldata);

        ani = ePlayerAni.SWIFT;
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

            HitMob.GetDamage(pCtrl.weaponDamage * skillData.damage);
            pAni.SetSkillSwift(2, 5);
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
