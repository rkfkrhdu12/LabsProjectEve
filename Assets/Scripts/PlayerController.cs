using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        InitMove();
        InitInventory();
        InitAttack();
    }

    void FixedUpdate()
    {
        UpdateInputKey();

        UpdateMove();

        UpdateAttack();
    }

    // Input
    #region Input

    void UpdateInputKey()
    {
        ePlayerAni ani = ePlayerAni.IDLE;
        z = 0;
        x = 0;

        // 공격시 이동불가
        if (isAttack) { return; }

        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventory = true;
        }

        // 인벤토리 오픈시 이동불가
        if(isInventory) { return; }

        if (Input.GetKey(KeyCode.W))
        {
            z += 1;
            ani = (ePlayerAni.WALK);
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1;
            ani = (ePlayerAni.WALK);
        }

        if (Input.GetKey(KeyCode.D))
        {
            z += 1 * .3f;
            x += 1;
            ani = (ePlayerAni.WALK);
        }
        if (Input.GetKey(KeyCode.A))
        {
            z += 1 * .3f;
            x -= 1;
            ani = (ePlayerAni.WALK);
        }
        z = Mathf.Clamp(z, -1, 1);
        x = Mathf.Clamp(x, -1, 1);

        if (Input.GetKey(KeyCode.LeftShift) && z != 0)
        {
            z *= 1.5f;
            ani = (ePlayerAni.RUN);
        }

        pAni.ChangeAni(ani);
    }
    #endregion

    // Move
    #region Move
    [SerializeField] float moveSpeed = 7;
    [SerializeField] float rotateSpeed = 60;

    public float z;
    float x;

    void InitMove()
    {
        z = 0;
        x = 0;
    }

    void UpdateMove()
    {
        x = x * rotateSpeed * Time.deltaTime;
        z = z * moveSpeed * Time.deltaTime;

        transform.Translate(0, 0, z);
        transform.Rotate(0, x, 0);
    }
    #endregion

    // Attack
    #region Attack
    public bool isAttack = false;
    public float health = 100;

    void InitAttack()
    {
        isAttack = false;
        health = 100;
    }

    void UpdateAttack()
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
            // 인벤토리 오픈시 이동불가
            if (isInventory) { return; }

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

    public void GetDamage(float damage)
    {
        health -= damage;
    }
    #endregion

    // Inventory
    bool isInventory = false;

    void InitInventory()
    {
        isInventory = false;
    }

    // Weapon

    // Animation
    #region Animation
    public PlayerAnimation pAni;

    #endregion
}