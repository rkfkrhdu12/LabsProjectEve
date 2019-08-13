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
    protected float maxHealth = 200;
    protected float healthRegen = 4;

    public float energyPoint = 100;
    protected float maxEnergy = 100;
    protected float energyRegen = 4;

    protected float regenTime = 0.0f;
    protected float regenInterval = 1.0f;

    protected float str = 100;

    public eDeadState eDeadState = eDeadState.NONE; 

    virtual public void GetDamage(float damage)
    {
        healthPoint -= damage;
    }
}
