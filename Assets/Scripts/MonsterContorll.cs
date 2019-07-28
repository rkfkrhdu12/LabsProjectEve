﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterContorll : MonoBehaviour
{
    //GameObject player;

    //NavMeshAgent nvAgent;
    //Animator _animator;

    //public float traceDist = 3.0f;
    //public float attackDist = 1.0f;

    //void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag ("Player");
    //    _animator = GetComponent<Animator>();
    //    nvAgent = GetComponent<NavMeshAgent>();

    //}

    //void Update()
    //{
    //    nvAgent.SetDestination(player.transform.position);

    //    AnimationUpdate();
    //}

    //void AnimationUpdate()
    //{
    //    if (nvAgent.destination != transform.position)
    //    {
    //        _animator.SetBool("isTrace", true);
    //    }
    //    else
    //    {
    //        _animator.SetBool("isTrace", false);
    //    }
    //}


    public enum CurrentState { idle, trace, attack, dead };
    public CurrentState curState = CurrentState.idle;

    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    public float traceDist = 3.0f;
    float attackDist = 1.0f;

    private bool isDead = false;

    void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
        nvAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        attackDist = nvAgent.stoppingDistance;

        //nvAgent.SetDestination(playerTransform.position);
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
        curAni = key_IsIdle;
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.01f);
            float dist = Vector3.Distance(playerTransform.position, transform.position);

            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    ChangeAnimation(key_IsIdle);
                    nvAgent.isStopped = true;
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;
                    nvAgent.isStopped = false;
                    ChangeAnimation(key_IsTrace);
                    break;
                case CurrentState.attack:
                    transform.LookAt(playerTransform);
                    ChangeAnimation(key_IsAttack);
                    break;
            }
            yield return null;
        }
    }

    string key_IsIdle = "isIdle";
    string key_IsTrace = "isTrace";
    string key_IsAttack = "isAttack";

    string curAni = "isTrace";
    void ChangeAnimation(string changeAni)
    {
        _animator.SetBool(curAni, false);
        if (changeAni != "isIdle")
        {
            curAni = changeAni;
            _animator.SetBool(curAni, true);
        }
    }

    public MonsterWeapon[] hand;

    float health = 100;
    public void GetDamage(float damage)
    {
        Debug.Log(1);
        health -= damage;
    }

    public void LeftHandAttackStart() { hand[0].ON(); }
    public void LeftHandAttackEnd() { hand[0].OFF(); }

    public void RightHandAttackStart() { hand[1].ON(); }
    public void RightHandAttackEnd() { hand[1].OFF(); }

}
