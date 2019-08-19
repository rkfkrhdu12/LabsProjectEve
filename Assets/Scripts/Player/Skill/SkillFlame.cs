using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlame : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.FLAME;
    }

    public override void UpdateSkill()
    {
        if (HitMob != null && !isEffect)
        {
            isEffect = true;
            HitMob.SetChainDamage(weaponDamage * .04f, 5);
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
