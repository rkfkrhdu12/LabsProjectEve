using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    PlayerHealthUI pHealthUI;
    public GameObject healthObj;

    InventoryUI invenUI;
    public GameObject inventoryObj;


    void Start()
    {
        pHealthUI = new PlayerHealthUI();
        pHealthUI.Start(healthObj);
        healthObj.SetActive(true);

        invenUI = new InventoryUI();
        invenUI.Start(inventoryObj);
        inventoryObj.SetActive(false);
    }

    public bool isInventory = false;

    float loadTime = 0.0f;
    float loadInterval = .5f;
    void Update()
    {
        if (!isInventory)
        {
            pHealthUI.Update();
        }
        else
        {
            loadTime += Time.deltaTime;
            if(loadTime > loadInterval)
            {
                loadTime = 0.0f;
                invenUI.Load();
            }
        }
    }

    public void OnInventory()
    {
        isInventory = true;
        inventoryObj.SetActive(true);
        invenUI.Load();

        healthObj.SetActive(false);
    }

    public void OffInventory()
    {
        isInventory = false;
        inventoryObj.SetActive(false);

        healthObj.SetActive(true);
    }
}
