using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.Instance.monsterTag))
        {
            Debug.Log(2);
            other.GetComponent<MonsterContorll>().GetDamage(damage);
        }
    }
}
