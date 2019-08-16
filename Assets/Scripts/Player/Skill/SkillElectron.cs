using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillElectron : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.ELECTRON;
        UINumber = 2;
    }

    public override void ReadySkill()
    {
        base.ReadySkill();
        
        pAnimator.speed = pCtrl.aniSpeed;

        if (HitMob != null)
            HitMob.SetParalysis(2);
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
