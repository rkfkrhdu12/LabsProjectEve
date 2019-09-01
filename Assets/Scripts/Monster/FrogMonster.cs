using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMonster : MonsterContorll
{
    protected override void Init()
    {
        base.Init();

        key_IsAttack = "IsJump";
    }

    protected override void UpdateTraceAction()
    {
        float dist = Vector3.Distance(playerTransform.position, transform.position);

        if (dist <= 2)
        {
            key_IsTrace = key_IsAttack;
        }
        else if (dist <= traceDist)
        {
            key_IsTrace = "IsTrace";
        }

        base.UpdateTraceAction();
    }
}
