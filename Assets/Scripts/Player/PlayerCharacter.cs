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
    float regenEnergy = 4;

    float regenTime = 0.0f;
    float regenInterval = 1.0f;

    PlayerController pCtrl;
    PlayerAnimation pAni;

    private void Awake()
    {
        pCtrl = GetComponent<PlayerController>();
        pAni = GetComponent<PlayerAnimation>();
    }

    public override void UpdateDeath()
    {
        switch (eDeadState)
        {
            case eDeadState.DEAD:
                pCtrl.isDeath = true;
                pAni.ChangeAni(ePlayerAni.DEAD);
                eDeadState = eDeadState.NODAMAGE;
                break;
            case eDeadState.NODAMAGE:
                noDamageTime += Time.deltaTime;
                if (noDamageTime > noDamageInterval)
                {
                    noDamageTime = 0.0f;

                    eDeadState = eDeadState.REVIVE;
                }
                break;
            case eDeadState.REVIVE:
                pCtrl.isDeath = false;
                pAni.ChangeAni(ePlayerAni.IDLE);
                healthPoint = maxHealth * reviveHealth;
                energyPoint = maxEnergy * reviveEnergy;

                eDeadState = eDeadState.NONE;
                break;
        }
    }

    void Update()
    {
        UpdateDeath();

        if (pCtrl.isDeath) return;

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

    float attackCoolTime = 1.0f;
    float attackInterval = 1.0f;

    float jumperrorTime = 0.0f;
    float jumperrorInterval = 0.15f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.monsterTag))
        {
            attackCoolTime += Time.deltaTime;
            if (attackCoolTime > attackInterval)
            {
                attackCoolTime = 0;

                GetDamage(collision.gameObject.GetComponent<MonsterContorll>().Str());
            }
        }

        if (pCtrl.isJump)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                jumperrorTime += Time.deltaTime;
                if (jumperrorTime > jumperrorInterval)
                {
                    jumperrorTime = 0;

                    pCtrl.isJump = false;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            pCtrl.isJump = false;
        }
    }
}
