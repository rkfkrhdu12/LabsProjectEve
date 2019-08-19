using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillElectron : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.ELECTRON;
    }

    public override void UpdateSkill()
    {
        if (HitMob != null && !isEffect)
        {
            isEffect = true;
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
