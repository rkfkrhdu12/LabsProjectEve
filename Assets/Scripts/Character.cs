using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDeadState
{
    NONE,
    DEAD,
    NODAMAGE,
    REVIVE,
}

public class Character : MonoBehaviour
{
    public float healthPoint = 200;
    public float maxHealth = 200;
    protected float regenHealth = 2;

    public eDeadState eDeadState = eDeadState.NONE; 

    virtual public void GetDamage(float damage)
    { 
        if (healthPoint <= 0 && eDeadState < eDeadState.DEAD) { Death(); }

        healthPoint -= damage;
    }

    private void LateUpdate()
    {
        if (healthPoint <= 0 && eDeadState < eDeadState.DEAD)
        {
            Death();
        }
    }

    virtual public void UpdateDeath()
    {

    }

    void Death()
    {
        if (eDeadState != eDeadState.NONE) return;

        eDeadState = eDeadState.DEAD;
    }
}
