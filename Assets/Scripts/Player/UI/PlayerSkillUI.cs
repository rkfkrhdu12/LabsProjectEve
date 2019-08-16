using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct skillUI
{
    public Image skillIcon;
    public GameObject coolTimeUI;
    public float coolTime;
}

public class PlayerSkillUI : PlayerUI
{
    public eSkill[] slotSkill = new eSkill[(int)eSkill.LAST - (int)eSkill.FLAME];

    skillUI[] skillUI = new skillUI[(int)eSkill.LAST - (int)eSkill.FLAME];

    SkillManager skillMgr;

    public override void Start(GameObject obj)
    {
        base.Start(obj);

        for (int i = 0; i < (int)eSkill.LAST - (int)eSkill.FLAME; ++i)
        {
            skillUI[i].skillIcon = obj.transform.GetChild(i + 1).GetComponent<Image>();
            skillUI[i].coolTimeUI = skillUI[i].skillIcon.transform.GetChild(0).gameObject;
            skillUI[i].coolTimeUI.SetActive(false);

            skillUI[i].coolTime = 0;
        }

        skillMgr = GameManager.Instance.player.GetComponent<SkillManager>();
    }

    public SkillCoolTime GetObj(int count)
    {
        return skillUI[count].coolTimeUI.GetComponent<SkillCoolTime>();
    }
    
    public SkillCoolTime GetSkillCoolTime(eSkillSlot eskill)
    {
        return GetObj((int)eskill);
    }
}
