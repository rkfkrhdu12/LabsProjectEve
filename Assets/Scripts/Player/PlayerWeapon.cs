using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float damage = 100;

    PlayerController pCtrl;

    void Start()
    {
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (pCtrl.weaponDamage != damage)
            pCtrl.weaponDamage = damage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.Instance.monsterTag))
        {
            if (!pCtrl.isAttack) return;

            pCtrl.isAttack = false;
            MonsterContorll mob = other.GetComponent<MonsterContorll>();
            pCtrl.SetAttack(mob);
        }
    }
}