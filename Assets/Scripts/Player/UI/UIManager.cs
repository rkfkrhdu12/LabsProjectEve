using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eUI
{
    HEALTH,
    ENERGY,
    SKILL,
    DATACHIP,
    INVENTORY,
    LAST,
}

public class UIManager : MonoBehaviour
{
    public GameObject playerUIObj;

    GameObject[] uiObj = new GameObject[(int)eUI.LAST];
    public UI[] pUI = new UI[(int)eUI.LAST];

    void Awake()
    {
        StartUI((int)eUI.HEALTH, new UIHealth(), true);

        StartUI((int)eUI.ENERGY, new UIEnergy(), true);

        StartUI((int)eUI.SKILL, new UISkill(), true);

        StartUI((int)eUI.DATACHIP, new UIDataChip(), true);

        StartUI((int)eUI.INVENTORY, new UIInventory(), false);

    }

    void StartUI(int nui,UI pui, bool isactive)
    {
        pUI[nui] = pui;
        uiObj[nui] = playerUIObj.transform.GetChild(nui).gameObject;

        pUI[nui].Start(uiObj[nui]);

        uiObj[nui].SetActive(isactive);
    }

    public bool isInventory = false;
    
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
            pUI[(int)eUI.INVENTORY].Update();
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

    public UISkill GetPlayerSkillUI()
    {
        return (UISkill)pUI[(int)eUI.SKILL];
    }

    public SkillCoolTime GetSkillCoolTime(eSkillSlot eskill)
    {
        return ((UISkill)pUI[(int)eUI.SKILL]).GetSkillCoolTime(eskill);
    }
}
