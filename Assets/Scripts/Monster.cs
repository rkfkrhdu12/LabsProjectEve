using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Player player;

    bool isPlayerCollision = false;

    [SerializeField] float moveSpeed = 5;
    
    void Start()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        if (isPlayerCollision)
        {
            // 플레이어가 범위안에 들어오면
            transform.LookAt(player.transform);

            transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerCollision = true;
            Debug.Log(1);
        }
    }

}
