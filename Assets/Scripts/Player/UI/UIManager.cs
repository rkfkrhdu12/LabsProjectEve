using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eUI
{
    HEALTH,
    ENERGY,
    INVENTORY,
    LAST,
}

public class UIManager : MonoBehaviour
{
    public GameObject playerUIObj;

    GameObject[] obj = new GameObject[(int)eUI.LAST];
    PlayerUI[] pUI = new PlayerUI[(int)eUI.LAST];

    void Start()
    {
        StartUI((int)eUI.HEALTH, new PlayerHealthUI(), true);

        StartUI((int)eUI.ENERGY, new PlayerEnergyUI(), true);

        StartUI((int)eUI.INVENTORY, new PlayerInventoryUI(), false);
    }

    void StartUI(int ui,PlayerUI pui, bool isactive)
    {
        pUI[ui] = pui;
        obj[ui] = playerUIObj.transform.GetChild(ui).gameObject;
        pUI[ui].Start(obj[ui]);
        obj[ui].SetActive(isactive);
    }

    public bool isInventory = false;

    float loadTime = 0.0f;
    float loadInterval = .5f;
    void Update()
    {
        if (!isInventory)
        {
            pUI[(int)eUI.HEALTH].Update();
            pUI[(int)eUI.ENERGY].Update();
        }
        else
        {
            loadTime += Time.deltaTime;
            if(loadTime > loadInterval)
            {
                loadTime = 0.0f;
                pUI[(int)eUI.INVENTORY].Update();
            }
        }
    }

    public void OnInventory()
    {
        isInventory = true;
        obj[(int)eUI.INVENTORY].SetActive(true);
        pUI[(int)eUI.INVENTORY].Update();

        obj[(int)eUI.HEALTH].SetActive(false);
        obj[(int)eUI.ENERGY].SetActive(false);
    }

    public void OffInventory()
    {
        isInventory = false;
        obj[(int)eUI.INVENTORY].SetActive(false);

        obj[(int)eUI.HEALTH].SetActive(true);
        obj[(int)eUI.ENERGY].SetActive(true);
    }
}
