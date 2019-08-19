using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SkillData
{
    public float coolTime;
    public float epPrice;
    public float damage;
}

public struct SkillSlot
{
    public SkillCoolTime coolTimeUI;
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
    PlayerCharacter pChar;
    public PlayerSkillUI pSkillUI;
    UIManager uiMgr;

    public Skill[] skill = new Skill[(int)eSkill.LAST];
    public SkillData[] skillData = new SkillData[(int)eSkill.LAST];
    SkillSlot[] slot = new SkillSlot[(int)eSkill.LAST -(int)eSkill.FLAME];

    public Sprite[] skillImage = new Sprite[(int)eSkill.LAST - (int)eSkill.FLAME];

    void InitData()
    {
        skillData[(int)eSkill.DEFAULT_ATTACK].coolTime = .7f;
        skillData[(int)eSkill.DEFAULT_ATTACK].epPrice = 0;
        skillData[(int)eSkill.DEFAULT_ATTACK].damage = 1;

        skillData[(int)eSkill.SHIFT].coolTime = 2f;
        skillData[(int)eSkill.SHIFT].epPrice = 0;

        // Skill

        skillData[(int)eSkill.FLAME].coolTime = 12f;
        skillData[(int)eSkill.FLAME].epPrice = 30;
        skillData[(int)eSkill.FLAME].damage = 1.2f;

        skillData[(int)eSkill.SWIFT].coolTime = 15f;
        skillData[(int)eSkill.SWIFT].epPrice = 40;
        skillData[(int)eSkill.SWIFT].damage = 1.4f;

        skillData[(int)eSkill.ELECTRON].coolTime = 10;
        skillData[(int)eSkill.ELECTRON].epPrice = 40;
        skillData[(int)eSkill.ELECTRON].damage = 1.1f;

        skillData[(int)eSkill.FREEZING].coolTime = 18;
        skillData[(int)eSkill.FREEZING].epPrice = 20;
        skillData[(int)eSkill.FREEZING].damage = 1.3f;
    }

    public void Awake()
    {
        InitData();

        uiMgr = GetComponent<UIManager>();
        pCtrl = GetComponent<PlayerController>();
        pChar = GetComponent<PlayerCharacter>();

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
        for (int i = (int)eSkill.DEFAULT_ATTACK; i < skill.Length; ++i)
        {
            skill[i].Init(skillData[i].coolTime);
        }

        SetSlotUI(eSkillSlot.A);
        SetSlotUI(eSkillSlot.S);
        SetSlotUI(eSkillSlot.D);
        SetSlotUI(eSkillSlot.F);

    }

    void Update()
    {
        UpdateSkillUI();
        UpdateCoolTime();
    }

    #region PublicSkillFuc

    public void SetA(eSkill skill) { SetSkill(skill, eSkillSlot.A); }
    public void A(MonsterContorll mob, float weapondamage)
    {

        ActiveSkill(eSkillSlot.A, mob, weapondamage);
    }

    public void SetS(eSkill skill) { SetSkill(skill, eSkillSlot.S); }
    public void S(MonsterContorll mob, float weapondamage)
    {
        ActiveSkill(eSkillSlot.S, mob, weapondamage);
    }

    public void SetD(eSkill skill) { SetSkill(skill, eSkillSlot.D); }
    public void D(MonsterContorll mob, float weapondamage)
    {
        ActiveSkill(eSkillSlot.D, mob, weapondamage);
    }

    public void SetF(eSkill skill) { SetSkill(skill,eSkillSlot.F); }
    public void F(MonsterContorll mob, float weapondamage)
    {
        ActiveSkill(eSkillSlot.F, mob, weapondamage);
    }
    #endregion

    void SetSlotUI(eSkillSlot eslot)
    {
        slot[(int)eslot].coolTimeUI = uiMgr.GetSkillCoolTime(eslot);
    }

    void SetSkill(eSkill eskill, eSkillSlot eslot)
    {
        slot[(int)eslot].skill = eskill;
        PlayerSkillUI pSkillUI = uiMgr.GetPlayerSkillUI();

        pSkillUI.GetImage(eslot).sprite = skillImage[(int)eslot];
        pSkillUI.slotSkill[(int)eslot] = eskill;
    }

    void CheckActiveSkill(eSkillSlot eslot)
    {
        if (skillData[(int)slot[(int)eslot].skill].epPrice > pChar.energyPoint) return;
        if (skill[(int)slot[(int)eslot].skill].isCool) return;
        if (slot[(int)eslot].skill == eSkill.NONE) return;

        pChar.energyPoint -= skillData[(int)slot[(int)eslot].skill].epPrice;
    }

    void ActiveSkill(eSkillSlot eslot, MonsterContorll mob, float weapondamage)
    {
        CheckActiveSkill(eslot);

        Debug.Log("0");

        pCtrl.skillCode = slot[(int)eslot].skill;
        pCtrl.curState = ePlayerState.SKILL;

        slot[(int)eslot].coolTimeUI.gameObject.SetActive(true);

        skill[(int)eslot + (int)eSkill.FLAME].isUI = true;
        skill[(int)eslot + (int)eSkill.FLAME].SetAttack(mob, weapondamage);
    }

    void UpdateSkillUI()
    {
        for (int i = 0; i < slot.Length; ++i)
        {
            if (!skill[i + (int)eSkill.FLAME].isCool && skill[i + (int)eSkill.FLAME].isUI)
            {
                skill[i + (int)eSkill.FLAME].isUI = false;
                slot[i].coolTimeUI.gameObject.SetActive(false);
            }
        }
    }

    void UpdateCoolTime()
    {
        for (int i = (int)eSkill.DEFAULT_ATTACK; i < skill.Length; ++i)
        {
            skill[i].UpdateCoolTime();
            if (i > 2)
                slot[i - 3].coolTimeUI.GetRemainTime(skill[i].GetRemainTime());
        }
    }
}
