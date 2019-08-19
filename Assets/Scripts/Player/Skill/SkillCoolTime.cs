using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    private float remainTime;

    private string skill;

    Text coolTimeText;

    void Awake()
    {
        coolTimeText = transform.GetChild(1).GetComponent<Text>();

        skill = transform.parent.name;
    }

    void Update()
    {
        coolTimeText.text = "" + (int)remainTime;
    }

    public void GetRemainTime(float remaintime)
    {
        remainTime = remaintime;
    }
}
