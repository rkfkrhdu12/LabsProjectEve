using UnityEngine;

public class SkillDefaultAttack : Skill
{
    public override void Init(float cool)
    {
        base.Init(cool);

        ani = ePlayerAni.ATTACK;
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

        base.End();
    }
}
