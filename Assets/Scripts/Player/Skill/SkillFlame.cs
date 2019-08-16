using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlame : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.FLAME;
        UINumber = 0;
    }

    public override void ReadySkill()
    {
        base.ReadySkill();

        pAnimator.speed = pCtrl.aniSpeed;
        if (HitMob != null)
            HitMob.SetChainDamage(weaponDamage * .04f, 5);
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
