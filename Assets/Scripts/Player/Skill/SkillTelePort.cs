using UnityEngine;

public class SkillTelePort : Skill
{
    int tpSize = 2;

    public override void Init()
    {
        base.Init();

        coolInterval = 2.0f;
    }

    public override void ReadySkill()
    {
        if (isCool) { End(); return; }

        isCool = true;
        base.ReadySkill();

        Vector3 dir =
            pCtrl.transform.localRotation * (Vector3.forward * tpSize);

        pCtrl.transform.position =
            new Vector3(pCtrl.transform.position.x + dir.x,
                        pCtrl.transform.position.y,
                        pCtrl.transform.position.z + dir.z);
        End();
    }
}
