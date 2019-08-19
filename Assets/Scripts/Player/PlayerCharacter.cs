using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    float noDamageTime = 0.0f;
    float noDamageInterval = 5.0f;
    float reviveHealth = .1f;
    float reviveEnergy = 0;

    public float energyPoint = 100;
    public float maxEnergy = 100;
    protected float regenEnergy = 4;

    protected float regenTime = 0.0f;
    protected float regenInterval = 1.0f;

    public PlayerController pCtrl;

    private void Awake()
    {
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    void Update()
    {
        switch (eDeadState)
        {
            case eDeadState.DEAD:
                eDeadState = eDeadState.NODAMAGE;
                break;
            case eDeadState.NODAMAGE:
                noDamageTime += Time.deltaTime;
                if(noDamageTime > noDamageInterval)
                {
                    noDamageTime = 0.0f;

                    eDeadState = eDeadState.REVIVE;
                }
                break;
            case eDeadState.REVIVE:
                eDeadState = eDeadState.NONE;
                healthPoint = maxHealth * reviveHealth;
                energyPoint = maxEnergy * reviveEnergy;
                break;
        }

        regenTime += Time.deltaTime;
        if(regenTime > regenInterval)
        {
            regenTime = 0.0f;

            healthPoint += regenHealth;
            energyPoint += regenEnergy;
        }
        healthPoint = Mathf.Clamp(healthPoint, 0, maxHealth);
        energyPoint = Mathf.Clamp(energyPoint, 0, maxEnergy);
    }

    public float GetStatus(int count)
    {
        switch (count)
        {
            case 0:
                return pCtrl.weaponDamage;
            case 1:
                return maxHealth;
            case 2:
                return maxEnergy;
            case 3:
                return regenHealth;
            case 4:
                return regenEnergy;
        }

        return -1;
    }
}
