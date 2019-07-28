using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        MoveInit();
    }

    void FixedUpdate()
    {
        Move();

        Attack();
    }

    // Move
    #region Move
    public float moveSpeed = 7;
    [SerializeField] float rotateSpeed = 60;

    public float z;
    const float maxZ = 1;
    float x;
    const float maxX = 1;

    bool isRun = false;

    void MoveInit()
    {
        z = 0;
        x = 0;
        isRun = false;
    }

    void MoveInput()
    {
        ePlayerAni ani = ePlayerAni.IDLE;
        z = 0;
        x = 0;

        // 공격시 이동불가
        if (isAttack) { return; }

        if (Input.GetKey(KeyCode.W))
        {
            z += maxZ;
            ani = (ePlayerAni.WALK);
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= maxZ;
            ani = (ePlayerAni.WALK);
        }

        if (Input.GetKey(KeyCode.D))
        {
            z += maxZ * .3f;
            x += maxX;
            ani = (ePlayerAni.WALK);
        }
        if (Input.GetKey(KeyCode.A))
        {
            z += maxZ * .3f;
            x -= maxX;
            ani = (ePlayerAni.WALK);
        }
        z = Mathf.Clamp(z, -maxZ, maxZ);
        x = Mathf.Clamp(x, -maxX, maxX);

        if (Input.GetKey(KeyCode.LeftShift) && z != 0)
        {
            z *= 1.5f;
            ani = (ePlayerAni.RUN);
        }

        pAni.ChangeAni(ani);
    }

    void Move()
    {
        MoveInput();

        x = x * rotateSpeed * Time.deltaTime;
        z = z * moveSpeed * Time.deltaTime;

        transform.Translate(0, 0, z);
        transform.Rotate(0, x, 0);
    }
    #endregion

    // Attack
    #region Attack
    public bool isAttack = false;

    float attackTime = 0.0f;
    float attackInterval = 1.0f;
    void Attack()
    {
        if (isAttack)
        {
            if (pAni.curAni == ePlayerAni.IDLE)
            {
                isAttack = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAttack = true;
                pAni.ChangeAni(ePlayerAni.ATTACK01);
            }

            if (Input.GetMouseButtonDown(1))
            {
                isAttack = true;
                pAni.ChangeAni(ePlayerAni.ATTACK02);
            }
        }
    }
    #endregion

    // Animation
    #region Animation
    public PlayerAnimation pAni;

    #endregion
}