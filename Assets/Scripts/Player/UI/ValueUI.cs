using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueUI : PlayerUI
{
   protected PlayerCharacter character;
    protected GameObject healthUIObj;

    protected Slider slider;
    protected const int nSliderObj = 0;

    protected Text text;
    protected const int nTextObj = 2;

    protected float value;
    protected float maxvalue;

    public override void Start(GameObject obj)
    {
        healthUIObj = obj;

        slider = healthUIObj.transform.GetChild(nSliderObj).GetComponent<Slider>();
        text = healthUIObj.transform.GetChild(nTextObj).GetComponent<Text>();

        character = GameManager.Instance.player.GetComponent<PlayerCharacter>();
    }

    public virtual void Init()
    {
    }

    public override void Update()
    {
        Init();

        slider.value = value;
        text.text = value + "/" + maxvalue;
    }
}
