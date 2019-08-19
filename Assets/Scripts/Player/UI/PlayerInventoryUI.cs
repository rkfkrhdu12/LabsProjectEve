using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventoryUI : PlayerUI
{
    public GameObject inventoryUIObj;
    PlayerCharacter pChar;

    const int UIHPnEP = 0;
    enum eHPnEP
    {
        HP,
        EP,
    }
    Slider hpSlider;
    Slider epSlider;

    const int UICharacternStatus = 1;
    const int statusCount = 5;
    Text[] statusText = new Text[statusCount];

    public override void Start(GameObject obj)
    {
        inventoryUIObj = obj;
        pChar = GameManager.Instance.player.GetComponent<PlayerCharacter>();

        InitHPNEP();
        InitStatus();
    }

    public override void Update()
    {
        UpdateHPnEP();
        UpdateStatus();
    }

    void InitHPNEP()
    {
        hpSlider = inventoryUIObj.transform.GetChild(UIHPnEP).GetChild((int)eHPnEP.HP).GetComponent<Slider>();
        hpSlider.maxValue = pChar.maxHealth;

        epSlider = inventoryUIObj.transform.GetChild(UIHPnEP).GetChild((int)eHPnEP.EP).GetComponent<Slider>();
        epSlider.maxValue = pChar.maxEnergy;
    }

    void InitStatus()
    {
        int UIstatus = 2;
        for (int i = 0; i < statusText.Length; ++i)
        {
            statusText[i] = inventoryUIObj.transform.GetChild(UICharacternStatus).GetChild(UIstatus).GetChild(i + 1).GetChild(0).GetComponent<Text>();
        }
    }

    void UpdateHPnEP()
    {
        hpSlider.value = pChar.healthPoint;
        epSlider.value = pChar.energyPoint;
    }

    void UpdateStatus()
    {
        for (int i = 0; i < statusText.Length; ++i)
        {
            float data = pChar.GetStatus(i);
            if (data == -1) statusText[i].text = "" + 0;
            else
            statusText[i].text = "" + data;
        }
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
