using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyUI : ValueUI
{
    public override void Init()
    {
        value = character.energyPoint;
        maxvalue = character.maxEnergy;

        base.Init();
    }
}
