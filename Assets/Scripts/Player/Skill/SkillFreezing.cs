using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFreezing : Skill
{
    int tpSize = 2;

    public override void Init(float cool)
    {
        base.Init(cool);

        UINumber = 3;
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
