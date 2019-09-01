using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMonster : MonsterContorll
{
    bool isDamaged = false;

    protected override void Init()
    {
        isDamaged = false;
        healthPoint = 100;
        maxHealth = healthPoint;
        str = 50;
    }

    public override float Str()
    {
        float dmg = base.Str();
        if (str != 0)
        {
            isDamaged = true;
        }
        return dmg;
    }

    protected override void UpdateAttack()
    {
        if (isDamaged)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.Instance.playerTag))
        {
            isAttackDamage = true;
        }
    }
}
