using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    private float coolInterval;
    private float coolTime;

    private string skill;

    Text coolTimeText;

    void Awake()
    {
        coolTime = 0.0f;
        isActive = false;
        coolTimeText = transform.GetChild(1).GetComponent<Text>();

        skill = transform.parent.name;
    }

    void Update()
    {
        if (!isActive) return;

        coolTimeText.text = "" + (int)(coolInterval - coolTime);

        coolTime += Time.deltaTime;
        if(coolTime > coolInterval)
        {
            coolTime = 0.0f;
            isActive = false;
        }
    }

    public void SetCoolTime(float cooltime)
    {
        coolInterval = cooltime;
    }

    bool isActive = false;
    public void Active()
    {
        isActive = true;
        coolTime = 0.0f;
    }
}
