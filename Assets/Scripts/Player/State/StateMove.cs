using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State
{
    float z;
    float x;
    float y;

    float moveSpeed = 5;
    float rotateSpeed = 180;
    float jumpPower = 4;

    Rigidbody rigid;
    Transform trns;

    public override void Init()
    {
        base.Init();

        rigid = pCtrl.GetComponent<Rigidbody>();
        trns = pCtrl.GetComponent<Transform>();

        z = 0;
        x = 0;
    }

    public override void UpdateState()
    {
        x = pCtrl.x * rotateSpeed * Time.deltaTime;
        z = pCtrl.z * moveSpeed * Time.deltaTime;

        trns.Rotate(0, x, 0);
        trns.Translate(0, 0, z);

        if(pCtrl.isJump)
        {
            y = pCtrl.y * jumpPower;

            if (y != 0)
            {
                Debug.Log(1);

                rigid.velocity = new Vector3(0, y, 0);

                pCtrl.y = 0;
            }
        }
    }
}
