using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMonster : MonsterContorll
{
    public override float Str()
    {
        

        return base.Str();
    }

    bool isColPlayer = false;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.Instance.playerTag))
        {

        }
    }
}
