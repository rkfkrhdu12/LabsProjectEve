using System.Collections;
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

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    public float traceDist = 3.0f;
    public float attackDist = 1.0f;

    private bool isDead = false;

    

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        playerTransform = GameManager.Instance.player.GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();

        //nvAgent.SetDestination(playerTransform.position);
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.01f);
            float dist = Vector3.Distance(playerTransform.position, _transform.position);

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
                    nvAgent.isStopped = true;
                    _animator.SetBool("isTrace", false);
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;
                    nvAgent.isStopped = false;
                    _animator.SetBool("isTrace", true);
                    break;
                case CurrentState.attack:
                    transform.LookAt(playerTransform);
                    break;
            }
            yield return null;
        }
    }
}




