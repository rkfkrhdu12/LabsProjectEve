using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    float damage = 8;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.Instance.playerTag))
        {
            other.GetComponent<PlayerCharacter>().GetDamage(damage);
        }
    }

    Collider weaponCol;

    void Start()
    {
        weaponCol = GetComponent<Collider>();
        OFF();
    }

    public void ON()
    {
        weaponCol.enabled = true;
    }

    public void OFF()
    {
        weaponCol.enabled = false;
    }
}
