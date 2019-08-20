using UnityEngine;

public class SkillDefaultAttack : Skill
{
    public override void Init(SkillData skilldata)
    {
        base.Init(skilldata);

        ani = ePlayerAni.ATTACK;
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
