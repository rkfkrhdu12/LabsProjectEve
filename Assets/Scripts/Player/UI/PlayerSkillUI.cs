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
    //public Image[] skillIcon = new Image[(int)eSkill.LAST - (int)eSkill.FLAME];
    //public GameObject[] coolTimeUI = new GameObject[(int)eSkill.LAST - (int)eSkill.FLAME];

    //public float[] coolTime = new float[(int)eSkill.LAST - (int)eSkill.FLAME];

    public float[] coolInterval = new float[(int)eSkill.LAST - (int)eSkill.FLAME];
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
            coolInterval[i] = 0;
        }

        skillMgr = GameManager.Instance.player.GetComponent<SkillManager>();
    }

    public override void Update()
    {
        base.Update();

        for (int i = 0; i < (int)eSkill.LAST - (int)eSkill.FLAME; ++i)
        {
            if (skillUI[i].coolTimeUI.activeSelf)
            {
                skillUI[i].coolTimeUI.transform.GetChild(1).GetComponent<Text>().text = "" + (int)(coolInterval[i] - skillUI[i].coolTime);

                skillUI[i].coolTime += Time.deltaTime;
                if (skillUI[i].coolTime > coolInterval[i])
                {
                    skillUI[i].coolTime = 0;
                }

            }
        }
    }

    public GameObject GetObj(int count)
    {
        return skillIcon[count].gameObject;
    }
    
    public GameObject A() { return GetObj(0); }
    
    public GameObject S() { return GetObj(1); }
    
    public GameObject D() { return GetObj(2); }
    
    public GameObject F() { return GetObj(3); }
}
