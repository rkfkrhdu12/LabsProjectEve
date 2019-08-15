using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkillData
{
    public float coolTime;
    public float epPrice;
    public float damage;
}

public struct SkillSlot
{
    public GameObject ui;
    public eSkill skill;
}

public enum eSkill
{
    NONE,
    DEFAULT_ATTACK, SHIFT,
    FLAME,
    SWIFT,
    ELECTRON,
    FREEZING,
    LAST,
}

public enum eSkillSlot
{
    A,S,D,F,
}

public class SkillManager : MonoBehaviour
{
    public PlayerController pCtrl;
    public PlayerSkillUI pSkillUI;

    public Skill[] skill = new Skill[(int)eSkill.LAST];
    public SkillData[] skillData = new SkillData[(int)eSkill.LAST];
    SkillSlot[] slot = new SkillSlot[(int)eSkill.LAST -(int)eSkill.FLAME]; 

    void InitData()
    {
        skillData[(int)eSkill.DEFAULT_ATTACK].coolTime = .7f;
        skillData[(int)eSkill.DEFAULT_ATTACK].epPrice = 0;
        skillData[(int)eSkill.DEFAULT_ATTACK].damage = 1;

        skillData[(int)eSkill.SHIFT].coolTime = 2f;
        skillData[(int)eSkill.SHIFT].epPrice = 0;

        skillData[(int)eSkill.FLAME].coolTime = 2f;
        skillData[(int)eSkill.FLAME].epPrice = 30;
        skillData[(int)eSkill.FLAME].damage = 1.2f;

        skillData[(int)eSkill.SWIFT].coolTime = 5f;
        skillData[(int)eSkill.SWIFT].epPrice = 40;
        skillData[(int)eSkill.SWIFT].damage = 1.4f;

        skillData[(int)eSkill.ELECTRON].coolTime = 0;
        skillData[(int)eSkill.ELECTRON].epPrice = 40;
        skillData[(int)eSkill.ELECTRON].damage = 1.1f;

        skillData[(int)eSkill.FREEZING].coolTime = 8;
        skillData[(int)eSkill.FREEZING].epPrice = 20;
        skillData[(int)eSkill.FREEZING].damage = 1.3f;
    }

    public void Awake()
    {
        InitData();

        skill = new Skill[(int)eSkill.LAST];

        skill[(int)eSkill.NONE] = null;
        skill[(int)eSkill.DEFAULT_ATTACK] = new SkillDefaultAttack();
        skill[(int)eSkill.SHIFT] = new SkillShift();
        skill[(int)eSkill.FLAME] = new SkillFlame();
        skill[(int)eSkill.SWIFT] = new SkillSwift();
        skill[(int)eSkill.ELECTRON] = new SkillElectron();
        skill[(int)eSkill.FREEZING] = new SkillFreezing();
    }

    public void Start()
    {
        for (int i = (int)eSkill.DEFAULT_ATTACK; i < (int)eSkill.LAST; ++i)
        {
            skill[i].Init(skillData[i].coolTime);
        }

        slot[(int)eSkillSlot.A].ui = GetComponent<UIManager>().GetPlayerSkillUI().A();
        slot[(int)eSkillSlot.S].ui = GetComponent<UIManager>().GetPlayerSkillUI().S();
        slot[(int)eSkillSlot.D].ui = GetComponent<UIManager>().GetPlayerSkillUI().D();
        slot[(int)eSkillSlot.F].ui = GetComponent<UIManager>().GetPlayerSkillUI().F();

        pCtrl = GetComponent<PlayerController>();
    }

    void Update()
    {
        UpdateSkillUI();

        for (int i = (int)eSkill.DEFAULT_ATTACK; i < (int)eSkill.LAST; ++i)
        {
            skill[i].UpdateCoolTime();
        }
    }
    
    public void SetA(eSkill skill) { SetSkill(skill, eSkillSlot.A); }
    public void A()
    {
        ActiveSkill(eSkillSlot.A);
    }

    public void SetS(eSkill skill) { SetSkill(skill, eSkillSlot.S); }
    public void S()
    {
        ActiveSkill(eSkillSlot.S);
    }

    public void SetD(eSkill skill) { SetSkill(skill, eSkillSlot.D); }
    public void D()
    {
        ActiveSkill(eSkillSlot.D);
    }

    public void SetF(eSkill skill) { SetSkill(skill,eSkillSlot.F); }
    public void F()
    {
        ActiveSkill(eSkillSlot.F);
    }

    public void SetSkill(eSkill eskill, eSkillSlot eslot)
    {
        slot[(int)eslot].skill = eskill;
        PlayerSkillUI pSkillUI = GetComponent<UIManager>().GetPlayerSkillUI();

        pSkillUI.coolInterval[(int)eslot] = skillData[(int)eslot].coolTime;
        pSkillUI.slotSkill[(int)eslot] = eskill;
    }

    public void ActiveSkill(eSkillSlot eslot)
    {
        if (slot[(int)eslot].skill == eSkill.NONE) return;

        pCtrl.skillCode = slot[(int)eslot].skill;
        pCtrl.curState = ePlayerState.SKILL;
        slot[(int)eslot].ui.transform.GetChild(0).gameObject.SetActive(true);
        skill[(int)eslot + (int)eSkill.FLAME].isUI = true;
    }

    void UpdateSkillUI()
    {
        for (int i = 0; i < (int)eSkill.LAST - (int)eSkill.FLAME; ++i)
        {
            if (!skill[i + (int)eSkill.FLAME].isCool && skill[i + (int)eSkill.FLAME].isUI)
            {
                skill[i + (int)eSkill.FLAME].isUI = false;
                slot[i].ui.transform.GetChild(0).gameObject.SetActive(false);
            }
            
        }
    }
}
