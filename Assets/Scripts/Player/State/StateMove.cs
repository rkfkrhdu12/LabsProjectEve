using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State
{
    float z;
    float x;

    public override void Init()
    {
        base.Init();

        z = 0;
        x = 0;
    }

    public override void UpdateState()
    {
        x = pCtrl.x * pCtrl.rotateSpeed * Time.deltaTime;
        z = pCtrl.z * pCtrl.moveSpeed * Time.deltaTime;

        pCtrl.transform.Rotate(0, x, 0);
        pCtrl.transform.Translate(0, 0, z);
    }
}
