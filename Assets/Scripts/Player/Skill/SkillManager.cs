using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Skill[] skill = new Skill[(int)eSkill.LAST];

    public void Awake()
    {
        skill = new Skill[(int)eSkill.LAST];

        skill[0] = null;
        skill[(int)eSkill.DEFAULT_ATTACK] = new SkillDefaultAttack();
        skill[(int)eSkill.TELEPORT] = new SkillTelePort();

    }

    public void Start()
    {
        for (int i = (int)eSkill.DEFAULT_ATTACK; i < (int)eSkill.LAST; ++i)
        {
            skill[i].Init();
        }
    }

    void Update()
    {
        for (int i = (int)eSkill.DEFAULT_ATTACK; i < (int)eSkill.LAST; ++i)
        {
            skill[i].UpdateCoolTime();
        }
    }
}
