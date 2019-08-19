using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwift : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);
        
        ani = ePlayerAni.SWIFT;
    }

    public override void UpdateSkill()
    {
        if (HitMob != null && !isEffect)
        {
            isEffect = true;
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
