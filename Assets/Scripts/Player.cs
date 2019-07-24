using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7;
    [SerializeField] float rotateSpeed = 60;

    int z;

    private void Start()
    {
        z = 0;
    }

    void Update()
    {
        z = 0;

        if(Input.GetKey(KeyCode.W))
        {
            z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1;
        }
        Mathf.Clamp(z, -1, 1);

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        transform.Translate(0, 0, z * moveSpeed * Time.deltaTime);
    }
}