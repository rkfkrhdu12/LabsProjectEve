using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChip : MonoBehaviour
{
    public GameObject UIObj;

    public bool isGet = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isGet) return;

        if (other.gameObject.CompareTag(GameManager.Instance.playerTag))
        {
            UIObj.SetActive(true);
        }
    }

    virtual protected void Teleport()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (!isGet)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                GameManager.Instance.dataChip++;
                UIObj.SetActive(false);
                Teleport();
                isGet = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.Instance.playerTag))
        {
            UIObj.SetActive(false);
        }
    }
}
