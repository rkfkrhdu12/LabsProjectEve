using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnergyHeal : Item
{
    protected override void Active()
    {
        PlayerCharacter pChar = GameManager.Instance.player.GetComponent<PlayerCharacter>();
        float heal = GameManager.Instance.itemEnergyHealing;

        float ep = pChar.energyPoint + heal;
        ep = Mathf.Clamp(ep, 0, pChar.maxEnergy);

        pChar.energyPoint = ep;
    }
}

