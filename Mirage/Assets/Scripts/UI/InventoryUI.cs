using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
{
    public GameObject item;
    public Image inventorySprite;
}

public class InventoryUI : SingletonPattern<InventoryUI>
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    [Header("Index")]
    public int index;
    public GameObject equippedItem;
    [SerializeField] private float panelScale = 1.4f;
    public List<GameObject> panels = new List<GameObject>();
    [SerializeField] private Color greyed;
    public bool hasKeys;

    private void Start()
    {
        UpdateItemPanels();
    }

    private void Update()
    {
        ReadInput();
        CheckItems();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setIndex(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setIndex(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setIndex(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            setIndex(3);
        }
    }

    public void changeIndex(int amountToChange)
    {
        index += amountToChange;
        if (index >= inventory.Count)
        {
            index = 0;
        }
        if (index < 0)
        {
            index = inventory.Count - 1;
        }
        equippedItem = inventory[index].item;
        UpdateItemPanels();
    }

    public void setIndex(int indexToSet)
    {
        index = indexToSet;
        if (index >= inventory.Count)
        {
            index = 0;
        }
        if (index < 0)
        {
            index = inventory.Count - 1;
        }
        equippedItem = inventory[index].item;
        UpdateItemPanels();
    }

    public void useItem()
    {

    }

    public void UpdateItemPanels()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i != index)
            {
                panels[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                panels[i].transform.localScale = new Vector3(panelScale, panelScale, 1.0f);
            }
        }
    }

    private void CheckItems()
    {
        // Coin Check
        if (SkillCheckTimer.Instance.hasCoin)
        {
            inventory[0].inventorySprite.color = Color.white;
            if (index == 0)
            {
                inventory[0].item.SetActive(true);
            }
            else
            {
                inventory[0].item.SetActive(false);
            }
        }
        else
        {
            inventory[0].inventorySprite.color = greyed;
            inventory[0].item.SetActive(false);
        }


        // Check Rock
        if (Pickup.Instance.carryObject)
        {
            inventory[1].inventorySprite.color = Color.white;
            if (index == 1)
            {
                inventory[1].item.SetActive(true);
            }
            else
            {
                inventory[1].item.SetActive(false);
            }
        }
        else
        {
            inventory[1].inventorySprite.color = greyed;
            inventory[1].item.SetActive(false);
        }


        // Compass
        if (Compass.Instance.hasCompass)
        {
            inventory[2].inventorySprite.color = Color.white;
            if (index == 2)
            {
                inventory[2].item.SetActive(true);
                if (PlayerStats.Instance.SanityPercent < 25)
                {
                    Compass.Instance.isCompassHallucinating = true;
                }
                else
                {
                    Compass.Instance.isCompassHallucinating = false;
                }
            }
            else
            {
                inventory[2].item.SetActive(false);
            }
        }
        else
        {
            inventory[2].inventorySprite.color = greyed;
            inventory[2].item.SetActive(false);
        }


        // Keys
        if (hasKeys)
        {
            inventory[3].inventorySprite.color = Color.white;
            if (index == 3)
            {
                inventory[3].item.SetActive(true);
            }
            else
            {
                inventory[3].item.SetActive(false);
            }
        }
        else
        {
            inventory[3].inventorySprite.color = greyed;
            inventory[3].item.SetActive(false);
        }
    }

}
