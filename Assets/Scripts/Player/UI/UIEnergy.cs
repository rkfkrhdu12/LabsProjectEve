using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : UIValue
{
    public override void Init()
    {
        value = character.energyPoint;
        maxvalue = character.maxEnergy;

        base.Init();
    }
}
