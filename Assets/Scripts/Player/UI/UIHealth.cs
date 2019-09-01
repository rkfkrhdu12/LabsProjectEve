using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : UIValue
{
    public override void Init()
    {
        value = character.healthPoint;
        maxvalue = character.maxHealth;

        value = Mathf.Clamp(value, 0, maxvalue);

        base.Init();
    }
}
