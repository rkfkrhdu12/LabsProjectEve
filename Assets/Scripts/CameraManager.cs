using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform player;

    [SerializeField] float moveSpeed = 7;

    private void Start()
    {
        player = GameManager.Instance.player.transform;

        isCol = false;
    }

    bool isCol = false;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * 2 * Time.deltaTime);
    }

    void OntriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            Debug.Log(1);
        }
    }
}