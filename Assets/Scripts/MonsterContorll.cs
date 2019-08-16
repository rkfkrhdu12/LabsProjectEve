using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterContorll : Character
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
    public NavMeshAgent nvAgent;
    private Animator _animator;

    public float traceDist = 3.0f;
    float attackDist = 1.0f;

    private bool isDead = false;

    bool isChainDamage = false;
    float chainDamage = 0.0f;
    float chainDamageTime = 0.0f;
    float chainDamageInterval = 0.0f;

    void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
        if (nvAgent == null)
            nvAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        attackDist = nvAgent.stoppingDistance;

        //nvAgent.SetDestination(playerTransform.position);
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
        curAni = key_IsIdle;

        healthPoint = 100;
        maxHealth = healthPoint;
        str = 10;
        isChainDamage = false;
    }

    void Update()
    {
        UpdateChainDamage();
        UpdateParalysis();
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
                    if (!isParalysis)
                    {
                        nvAgent.destination = playerTransform.position;
                        nvAgent.isStopped = false;
                        ChangeAnimation(key_IsTrace);
                    }
                    else
                    {
                        ParalysisEffect();
                    }
                    break;
                case CurrentState.attack:
                    transform.LookAt(playerTransform);
                    ChangeAnimation(key_IsAttack);
                    break;
            }
            yield return null;
        }
    }

    string key_IsIdle = "IsIdle";
    string key_IsTrace = "IsTrace";
    string key_IsAttack = "IsAttack";

    string curAni = "IsTrace";
    void ChangeAnimation(string changeAni)
    {
        _animator.SetBool(curAni, false);
        if (changeAni != key_IsIdle)
        {
            curAni = changeAni;
            _animator.SetBool(curAni, true);
        }
    }

    /// 
    public MonsterWeapon[] hand;
    
    public void LeftHandAttackStart() { hand[0].ON(); }
    public void LeftHandAttackEnd() { hand[0].OFF(); }

    public void RightHandAttackStart() { hand[1].ON(); }
    public void RightHandAttackEnd() { hand[1].OFF(); }
    
    public float Str()
    {
        return str;
    }

    public bool isParalysis = false;
    float paralysisTime = 0.0f;
    float paralysisInterval = 0.0f;
    public void SetParalysis(float paralysisinterval)
    {
        isParalysis = true;
        paralysisInterval = paralysisinterval;
    }

    public void UpdateParalysis()
    {
        if (!isParalysis) return;

        Debug.Log("isParalysis " +  isParalysis);

        paralysisTime += Time.deltaTime;
        if (paralysisInterval < paralysisTime)
        {
            Debug.Log("isParalysis End");

            paralysisTime = 0.0f;
            isParalysis = false;
        }
    }

    bool isParalysisEffect = false;
    void ParalysisEffect()
    {
        if (isParalysisEffect)
        {
            isParalysisEffect = false;
            ChangeAnimation(key_IsIdle);
        }
        else
        {
            isParalysisEffect = true;
            ChangeAnimation(key_IsTrace);
        }
    }

    public void SetChainDamage(float chaindamage, float chaindamageinterval)
    {
        isChainDamage = true;
        chainDamage = chaindamage;
        chainDamageInterval = chaindamageinterval;
    }

    float chaning = 1.0f;

    void UpdateChainDamage()
    {
        if (!isChainDamage) return;

        chainDamageTime += Time.deltaTime;
        if (chaning < chainDamageTime)
        {
            GetDamage(chainDamage);
            if (chainDamageInterval < chainDamageTime)
            {
                chainDamageTime = 0.0f;
                isChainDamage = false;
            }
            chaning++;
        }
    }

    public void Freezing()
    {
        transform.Translate(0,0,-.2f);
    }
}
