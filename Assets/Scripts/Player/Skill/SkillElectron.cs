using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillElectron : Skill
{
    public override void Init(SkillData skilldata)
    {
        base.Init(skilldata);

        ani = ePlayerAni.ELECTRON;
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
            HitMob.SetParalysis(2);
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
