using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float health = 100;
    public void GetDamage(float damage)
    {
        health -= damage;
    }
}
