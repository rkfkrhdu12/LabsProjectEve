using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    PlayerController player;
    public GameObject healthUIObj;

    Slider slider;
    Text text;

    void Start()
    {
        player = GetComponent<PlayerController>();

        slider = healthUIObj.transform.GetChild(1).GetComponent<Slider>();
        text = healthUIObj.transform.GetChild(2).GetComponent<Text>();
    }

    float health;
    void Update()
    {
        health = player.health;

        slider.value = 100 - health;
        text.text = health + "/" + 100;
    }

}
