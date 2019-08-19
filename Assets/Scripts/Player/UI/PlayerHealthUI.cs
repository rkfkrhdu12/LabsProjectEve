using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : ValueUI
{
    public override void Init()
    {
        value = character.healthPoint;
        maxvalue = character.maxHealth;

        base.Init();
    }
}
