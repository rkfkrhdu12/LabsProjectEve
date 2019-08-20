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
    protected float regenHealth = 4;

    public eDeadState eDeadState = eDeadState.NONE; 

    virtual public void GetDamage(float damage)
    {
        healthPoint -= damage;
    }

    private void LateUpdate()
    {
        if(healthPoint < 0)
        {
            
        }
    }

    virtual public void UpdateDeath()
    {

    }

    void Death()
    {
        GetComponent<Collider>().enabled = false;
        eDeadState = eDeadState.DEAD;
    }
}
