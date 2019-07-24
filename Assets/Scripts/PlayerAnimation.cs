using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    private const string key_isWalk = "IsWalk";
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    private const string key_isReset = "IsReset";

    [SerializeField] bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            animator.SetBool(key_isDead, true);

            return;
        }

        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool(key_isRun, true);
                animator.SetBool(key_isWalk, false);
            }
            else
            {
                animator.SetBool(key_isWalk, true);
                animator.SetBool(key_isRun, false);
            }
        }
        else
        {
            animator.SetBool(key_isWalk, false);
            animator.SetBool(key_isRun, false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(key_isAttack01, true);
        }
        else
        {
            animator.SetBool(key_isAttack01, false);
        }
		
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool(key_isAttack02, true);
        }
        else
        {
            animator.SetBool(key_isAttack02, false);
        }
       
        if (Input.GetKeyUp("space"))
        {
            animator.SetBool(key_isJump, true);
        }
        else
        {
            animator.SetBool(key_isJump, false);
        }
    }

    public void Reset()
    {
        isDead = false;

        animator.SetBool(key_isDead, false);
        animator.SetBool(key_isJump, false);
        animator.SetBool(key_isAttack02, false);
        animator.SetBool(key_isAttack01, false);
        animator.SetBool(key_isRun, false);
        animator.SetTrigger(key_isReset);
    }

    public void Hit()
    {
        animator.SetTrigger(key_isDamage);
    }
}