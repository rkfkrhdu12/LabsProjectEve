using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7;
    [SerializeField] float rotateSpeed = 60;

    bool isDash = false;

    public float z;

    private void Start()
    {
        z = 0;
    }

    void Update()
    {
        isDash = false;
        z = 0;

        if (Input.GetKey(KeyCode.W))
        {
            z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDash = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            z += .3f;
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            z += .3f;
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        z = Mathf.Clamp(z, -1f, 1f);

        z = ((z * moveSpeed) * (isDash == true ? 1.5f : 1.0f)) * Time.deltaTime;

        transform.Translate(0, 0, z);
    }
}