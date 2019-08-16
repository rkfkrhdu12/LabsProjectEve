using UnityEngine;

public class SkillDefaultAttack : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.ATTACK;
    }

    public override void ReadySkill()
    {
        base.ReadySkill();

        pAnimator.speed = pCtrl.aniSpeed;
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
