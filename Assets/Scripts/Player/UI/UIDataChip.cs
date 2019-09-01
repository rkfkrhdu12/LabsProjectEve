using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDataChip : UI
{
    Text datachipText;

    public override void Start(GameObject obj)
    {
        base.Start(obj);

        datachipText = obj.transform.GetChild(1).GetComponent<Text>();
    }

    public override void Update()
    {
        datachipText.text = "얻은 데이터칩의 갯수 : " + GameManager.Instance.dataChip;
    }
}
