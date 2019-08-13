using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI
{
    public GameObject inventoryUIObj;
    PlayerCharacter pChar;

    Slider slider;
    

    public void Start(GameObject obj)
    {
        inventoryUIObj = obj;
        pChar = GameManager.Instance.player.GetComponent<PlayerCharacter>();
        slider = inventoryUIObj.transform.GetChild(0).GetComponent<Slider>();
    }

    void LoadHealth()
    {
        slider.value = 100 - pChar.healthPoint;
    }

    public void Load()
    {
        LoadHealth();
    }

    public void OnUI()
    {
        inventoryUIObj.SetActive(true);
        Load();
    }

    public void OffUI()
    {

    }
}
