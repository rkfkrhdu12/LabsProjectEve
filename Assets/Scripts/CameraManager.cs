using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform player;
    Camera mainCamera;
    
    [SerializeField] float moveSpeed = 7;

    private void Start()
    {
        player = GameManager.Instance.player.transform;

        mainCamera = Camera.main;
        isCol = false;

        prevPlayerPos = Vector3.zero;
    }

    bool isCol = false;
    Vector3 prevPlayerPos;

    void Update()
    {
        if (!isCol)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isCol = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCol = false;
        }
    }

}