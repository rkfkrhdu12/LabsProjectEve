using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    float damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.Instance.monsterTag))
        {
            other.GetComponent<MonsterContorll>().GetDamage(damage);
        }
    }
}
