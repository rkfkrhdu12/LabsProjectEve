using UnityEngine;

public class SkillDefaultAttack : Skill
{
    public override void ReadySkill()
    {
        base.ReadySkill();

        pAni.ChangeAni(ePlayerAni.ATTACK01);
    }

    public override void UpdateSkill()
    {
        if (pAni.curAni == ePlayerAni.IDLE)
        {
            pAni.ChangeAni(ePlayerAni.IDLE);
            End();
        }
    }
}
