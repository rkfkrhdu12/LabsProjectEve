using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI
{
    public Character character;
    GameObject healthUIObj;

    Slider slider;
    Text text;

    public void Start(GameObject obj)
    {
        healthUIObj = obj;

        slider = healthUIObj.transform.GetChild(1).GetComponent<Slider>();
        text = healthUIObj.transform.GetChild(2).GetComponent<Text>();

        Init();
    }

    public virtual void Init()
    {
        
    }

    float health;
    public void Update()
    {
        health = character.healthPoint;

        slider.value = 100 - health;
        text.text = health + "/" + 100;
    }

}
