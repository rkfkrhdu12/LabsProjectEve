using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SkillData
{
    public float coolTime;
    public float epPrice;
    public float damage;
    public Color color;
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
    A, S, D, F,
}

public class SkillManager : MonoBehaviour
{
    public PlayerController pCtrl;
    PlayerCharacter pChar;
    public UISkill pUISkill;
    UIManager uiMgr;

    public Skill[] skills = new Skill[(int)eSkill.LAST];
    public SkillData[] data = new SkillData[(int)eSkill.LAST];
    SkillSlot[] slot = new SkillSlot[(int)eSkill.LAST - (int)eSkill.FLAME];

    public Sprite[] image = new Sprite[(int)eSkill.LAST - (int)eSkill.FLAME];

    public GameObject effectObj;

    void InitData()
    {
        data[(int)eSkill.DEFAULT_ATTACK].coolTime = .7f;
        data[(int)eSkill.DEFAULT_ATTACK].epPrice = 0;
        data[(int)eSkill.DEFAULT_ATTACK].damage = 1;
        data[(int)eSkill.DEFAULT_ATTACK].color = new Color(83f / 255f, 167f / 255f, 1);

        data[(int)eSkill.SHIFT].coolTime = 2f;
        data[(int)eSkill.SHIFT].epPrice = 0;
        data[(int)eSkill.DEFAULT_ATTACK].color = new Color(1, 1, 1);

        // Skill

        data[(int)eSkill.FLAME].coolTime = 12f;
        data[(int)eSkill.FLAME].epPrice = 30;
        data[(int)eSkill.FLAME].damage = 1.2f;
        data[(int)eSkill.FLAME].color = Color.red; //new Color(1, 137f / 255f, 76f / 255f);

        data[(int)eSkill.SWIFT].coolTime = 15f;
        data[(int)eSkill.SWIFT].epPrice = 40;
        data[(int)eSkill.SWIFT].damage = 1.4f;
        data[(int)eSkill.SWIFT].color = Color.green;//new Color(76f / 255f, 1, 183f / 255f);

        data[(int)eSkill.ELECTRON].coolTime = 10;
        data[(int)eSkill.ELECTRON].epPrice = 40;
        data[(int)eSkill.ELECTRON].damage = 1.1f;
        data[(int)eSkill.ELECTRON].color = Color.magenta; //new Color(217f / 255f, 76f / 255f, 1);

        data[(int)eSkill.FREEZING].coolTime = 18;
        data[(int)eSkill.FREEZING].epPrice = 20;
        data[(int)eSkill.FREEZING].damage = 1.3f;
        data[(int)eSkill.FREEZING].color = new Color(0, 1, 1);
    }

    public void Awake()
    {
        InitData();

        uiMgr = GetComponent<UIManager>();
        pCtrl = GetComponent<PlayerController>();
        pChar = GetComponent<PlayerCharacter>();

        skills = new Skill[(int)eSkill.LAST];

        skills[(int)eSkill.NONE] = null;
        skills[(int)eSkill.DEFAULT_ATTACK] = new SkillDefaultAttack();
        skills[(int)eSkill.SHIFT] = new SkillShift();
        skills[(int)eSkill.FLAME] = new SkillFlame();
        skills[(int)eSkill.SWIFT] = new SkillSwift();
        skills[(int)eSkill.ELECTRON] = new SkillElectron();
        skills[(int)eSkill.FREEZING] = new SkillFreezing();

        effectObj.SetActive(false);
    }

    public void Start()
    {
        for (int i = (int)eSkill.DEFAULT_ATTACK; i < skills.Length; ++i)
        {
            skills[i].Init(data[i]);
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

    public void SetF(eSkill skill) { SetSkill(skill, eSkillSlot.F); }
    public void F()
    {
        ActiveSkill(eSkillSlot.F);
    }
    #endregion

    void SetSlotUI(eSkillSlot eslot)
    {
        slot[(int)eslot].coolTimeUI = uiMgr.GetSkillCoolTime(eslot);
    }

    void SetSkill(eSkill eskill, eSkillSlot eslot)
    {
        slot[(int)eslot].skill = eskill;
        UISkill pSkillUI = uiMgr.GetPlayerSkillUI();

        pSkillUI.GetImage(eslot).sprite = image[(int)eslot];
        pSkillUI.slotSkill[(int)eslot] = eskill;
    }

    void ActiveSkill(eSkillSlot eslot)
    {
        if (data[(int)slot[(int)eslot].skill].epPrice > pChar.energyPoint) return;
        if (skills[(int)slot[(int)eslot].skill].isCool) return;
        if (slot[(int)eslot].skill == eSkill.NONE) return;

        pChar.energyPoint -= data[(int)slot[(int)eslot].skill].epPrice;

        pCtrl.skillCode = slot[(int)eslot].skill;
        pCtrl.curState = ePlayerState.SKILL;

        eslotValue = (int)eslot;
        slot[(int)eslot].coolTimeUI.gameObject.SetActive(true);

        skills[(int)eslot + (int)eSkill.FLAME].isUI = true;
    }

    public void Attack(MonsterContorll mob)
    {
        pCtrl.skillCode = eSkill.DEFAULT_ATTACK;
        pCtrl.curState = ePlayerState.SKILL;
    }

    void UpdateSkillUI()
    {
        for (int i = 0; i < slot.Length; ++i)
        {
            if (!skills[i + (int)eSkill.FLAME].isCool && skills[i + (int)eSkill.FLAME].isUI)
            {
                skills[i + (int)eSkill.FLAME].isUI = false;
                slot[i].coolTimeUI.gameObject.SetActive(false);
            }
        }
    }

    void UpdateCoolTime()
    {
        for (int i = (int)eSkill.DEFAULT_ATTACK; i < skills.Length; ++i)
        {
            skills[i].UpdateCoolTime();
            if (i > 2)
                slot[i - 3].coolTimeUI.GetRemainTime(skills[i].GetRemainTime());
        }
    }
    int eslotValue = 0;
    public void OnEffect()
    {
        effectObj.SetActive(true);
        effectObj.GetComponent<MeshRenderer>().sharedMaterial.color = data[(int)slot[eslotValue].skill].color;
    }

    public void OffEffect()
    {
        effectObj.SetActive(false);
    }
}
