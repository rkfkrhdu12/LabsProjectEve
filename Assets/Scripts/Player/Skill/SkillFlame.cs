using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlame : Skill
{

    public override void Init(SkillData skilldata)
    {
        base.Init(skilldata);

        ani = ePlayerAni.FLAME;
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
            HitMob.SetChainDamage(pCtrl.weaponDamage * .04f, 5);
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
