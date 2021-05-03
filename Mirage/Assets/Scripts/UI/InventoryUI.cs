using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
{
    public bool isUnlocked;
    public GameObject item;
    public Sprite inventorySprite;
}

public class InventoryUI : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public int index;
    public GameObject equippedItem;
    [SerializeField] private float panelScale = 1.4f;
    public List<GameObject> panels = new List<GameObject>();

    private void Start()
    {
        UpdateItemPanels();
    }

    private void Update()
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
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            setIndex(4);
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

    

}
