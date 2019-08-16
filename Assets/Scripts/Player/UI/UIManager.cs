using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eUI
{
    HEALTH,
    ENERGY,
    SKILL,
    INVENTORY,
    LAST,
}

public class UIManager : MonoBehaviour
{
    public GameObject playerUIObj;

    GameObject[] uiObj = new GameObject[(int)eUI.LAST];
    public PlayerUI[] pUI = new PlayerUI[(int)eUI.LAST];

    void Awake()
    {
        StartUI((int)eUI.HEALTH, new PlayerHealthUI(), true);

        StartUI((int)eUI.ENERGY, new PlayerEnergyUI(), true);

        StartUI((int)eUI.SKILL, new PlayerSkillUI(), true);

        StartUI((int)eUI.INVENTORY, new PlayerInventoryUI(), false);
    }

    void StartUI(int nui,PlayerUI pui, bool isactive)
    {
        pUI[nui] = pui;
        uiObj[nui] = playerUIObj.transform.GetChild(nui).gameObject;

        pUI[nui].Start(uiObj[nui]);

        uiObj[nui].SetActive(isactive);
    }

    public bool isInventory = false;

    float loadTime = 0.0f;
    float loadInterval = .5f;
    void Update()
    {
        if (!isInventory)
        {
            for (int i = 0; i < (int)eUI.INVENTORY; ++i)
            {
                pUI[i].Update();
            }
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
        uiObj[(int)eUI.INVENTORY].SetActive(true);
        pUI[(int)eUI.INVENTORY].Update();

        for (int i = 0; i < (int)eUI.INVENTORY; ++i)
        {
            uiObj[i].SetActive(false);
        }
    }

    public void OffInventory()
    {
        isInventory = false;
        uiObj[(int)eUI.INVENTORY].SetActive(false);

        for (int i = 0; i < (int)eUI.INVENTORY; ++i)
        {
            uiObj[i].SetActive(true);
        }
    }

    public PlayerSkillUI GetPlayerSkillUI()
    {
        return (PlayerSkillUI)pUI[(int)eUI.SKILL];
    }

    public SkillCoolTime GetSkillCoolTime(eSkillSlot eskill)
    {
        return ((PlayerSkillUI)pUI[(int)eUI.SKILL]).GetSkillCoolTime(eskill);
    }
}
