﻿using UnityEngine;

public class SkillShift : Skill
{
    int tpSize = 2;

    public override void ReadySkill()
    {
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
