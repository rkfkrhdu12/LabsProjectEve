using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : Item
{
    protected override void Active()
    {
        PlayerCharacter pChar = GameManager.Instance.player.GetComponent<PlayerCharacter>();
        float heal = GameManager.Instance.itemHealthHealing;

        float hp = pChar.healthPoint + heal;
        hp = Mathf.Clamp(hp, 0, pChar.maxHealth);

        pChar.healthPoint = hp;
    }
}
