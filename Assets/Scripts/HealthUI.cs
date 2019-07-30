using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    protected Character character;
    public GameObject healthUIObj;

    Slider slider;
    Text text;

    void Start()
    {
        slider = healthUIObj.transform.GetChild(1).GetComponent<Slider>();
        text = healthUIObj.transform.GetChild(2).GetComponent<Text>();

        Init();
    }

    public virtual void Init()
    {
        character = GetComponent<MonsterContorll>();
    }

    float health;
    void Update()
    {
        health = character.health;

        slider.value = 100 - health;
        text.text = health + "/" + 100;
    }

}
