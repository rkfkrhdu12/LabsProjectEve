using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : PlayerUI
{
    public GameObject inventoryUIObj;
    PlayerCharacter pChar;

    Slider slider;

    public override void Start(GameObject obj)
    {
        inventoryUIObj = obj;
        pChar = GameManager.Instance.player.GetComponent<PlayerCharacter>();
        slider = inventoryUIObj.transform.GetChild(0).GetComponent<Slider>();
    }

    void LoadHealth()
    {
        slider.value = 100 - pChar.healthPoint;
    }

    public override void Update()
    {
        LoadHealth();
    }

    public void OnUI()
    {
        inventoryUIObj.SetActive(true);
        Update();
    }

    public void OffUI()
    {

    }
}
