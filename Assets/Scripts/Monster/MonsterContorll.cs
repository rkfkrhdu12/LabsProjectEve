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

    public enum CurrentState { idle, trace, attack, hit, dead };
    public CurrentState curState = CurrentState.idle;

    protected Transform playerTransform;
    public NavMeshAgent nvAgent;
    private Animator _animator;

    public float traceDist = 6.0f;
    float attackDist = 1.0f;

    private bool isDead = false;

    protected float str = 10;

    bool isChainDamage = false;
    float chainDamage = 0.0f;
    float chainDamageTime = 0.0f;
    float chainDamageInterval = 0.0f;

    void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
        if (nvAgent == null)
            nvAgent = GetComponent<NavMeshAgent>();
        if (!gameObject.CompareTag(GameManager.Instance.monsterTag))
        {
            gameObject.tag = GameManager.Instance.monsterTag;
        }

        _animator = GetComponent<Animator>();

        attackDist = nvAgent.stoppingDistance;

        //nvAgent.SetDestination(playerTransform.position);
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
        curAni = key_IsIdle;

        isChainDamage = false;

        Init();
    }

    void Update()
    {
        UpdateDeath();

        UpdateAttack();

        UpdateChainDamage();
        UpdateParalysis();
    }

    virtual protected void Init()
    {

    }

    virtual protected void UpdateAttack()
    {

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
                    UpdateTraceAction();
                    break;
                case CurrentState.attack:
                    UpdateAttackAction();
                    break;
                case CurrentState.hit:
                    ChangeAnimation(key_IsHit);
                    break;
            }
            yield return null;
        }
    }

    virtual protected void UpdateTraceAction()
    {
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
    }

    virtual protected void UpdateAttackAction()
    {
        Vector3 targetPos = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

        transform.LookAt(targetPos);
        ChangeAnimation(key_IsAttack);
    }

    string key_IsIdle = "IsIdle";
    protected string key_IsTrace = "IsTrace";
    protected string key_IsAttack = "IsAttack";
    string key_IsHit = "IsHit";
    string key_IsDeath = "IsDeath";

    string curAni = "IsTrace";
    protected void ChangeAnimation(string changeAni)
    {
        _animator.SetBool(curAni, false);
        if (changeAni != key_IsIdle)
        {
            curAni = changeAni;
            _animator.SetBool(curAni, true);
        }
    }

    /// 


    virtual public float Str()
    {
        if (!isAttackDamage) return 0;

        isAttackDamage = false;
        return str;
    }

    public bool isAttackDamage = false;
    public void Attack()
    {
        isAttackDamage = true;
    }

    float deadTime = 0.0f;
    float deadInvertal = .8f;

    public override void UpdateDeath()
    {
        switch (eDeadState)
        {
            case eDeadState.NONE:
                if (healthPoint <= 0)
                {
                    eDeadState = eDeadState.DEAD;
                }
                break;
            case eDeadState.DEAD:
                curState = CurrentState.dead;

                ChangeAnimation(key_IsDeath);

                deadTime += Time.deltaTime;
                if (deadTime >= deadInvertal)
                {
                    eDeadState = eDeadState.NODAMAGE;
                }
                break;
            case eDeadState.NODAMAGE:
                gameObject.SetActive(false);
                break;
        }
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

        paralysisTime += Time.deltaTime;
        if (paralysisInterval < paralysisTime)
        {
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
        chaning = 1.0f;
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
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -2f);
    }

    public override void GetDamage(float damage)
    {
        curState = CurrentState.hit;

        base.GetDamage(damage);
    }
}
