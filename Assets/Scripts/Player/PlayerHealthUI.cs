using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : HealthUI
{
    public override void Init()
    {
        character = GetComponent<PlayerController>();
    }

}
