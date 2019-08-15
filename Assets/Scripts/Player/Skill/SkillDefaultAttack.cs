using UnityEngine;

public class SkillDefaultAttack : Skill
{
    public override void ReadySkill()
    {
        base.ReadySkill();

        pAni.ChangeAni(ePlayerAni.ATTACK01);
        isCool = true;
    }

    private const string key_isAttack01 = "IsAttack01";
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
