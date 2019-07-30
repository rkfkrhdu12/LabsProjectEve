using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI
{
    public GameObject inventoryUIObj;
    PlayerController pCtrl;

    Slider slider;
    

    public void Start(GameObject obj)
    {
        inventoryUIObj = obj;
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
        slider = inventoryUIObj.transform.GetChild(0).GetComponent<Slider>();
    }

    void LoadHealth()
    {
        slider.value = 100 - pCtrl.health;
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
