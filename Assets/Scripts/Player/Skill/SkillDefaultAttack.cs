using UnityEngine;

public class SkillDefaultAttack : Skill
{
    public override void Init()
    {
        base.Init();
        isCool = false;
        coolInterval = .7f;
    }

    public override void ReadySkill()
    {
        base.ReadySkill();

        pAni.ChangeAni(ePlayerAni.ATTACK01);
        isCool = true;
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
        pAni.ChangeAni(ePlayerAni.IDLE);

        base.End();
    }
}
