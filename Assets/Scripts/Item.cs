using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    virtual protected void Active()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.Instance.playerTag))
        {
            Active();
            gameObject.SetActive(false);
        }
    }
}
