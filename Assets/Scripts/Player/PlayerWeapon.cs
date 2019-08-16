using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float damage = 10;

    PlayerController pCtrl;

    public void Start()
    {
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pCtrl.isAttack) return;

        pCtrl.isAttack = false;
        if(other.gameObject.CompareTag(GameManager.Instance.monsterTag))
        {
            MonsterContorll mob = other.GetComponent<MonsterContorll>();
            pCtrl.SetAttack(mob, damage);
            mob.GetDamage(damage);
        }
    }
}
