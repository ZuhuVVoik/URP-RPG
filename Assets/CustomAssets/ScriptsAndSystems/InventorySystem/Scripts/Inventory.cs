using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /* Локальный инвентарь для сундучков етс */

    public List<ItemInstance> items = new List<ItemInstance>();
    public int space = 20;


    public bool AddItem(ItemInstance item)
    {
        if (!item.item.isDefaultItem)
        {
            if (this.items.Count <= space)
            {
                this.items.Add(item);


                return true;
            }
            else
            {
                Debug.Log("Inventory is full");
                return false;
            }
        }
        return false;
    }

    public void RemoveItem(ItemInstance item)
    {
        this.items.Remove(item);
    }

    public void SaveItems(InventorySlot[] slots)
    {
        this.items.Clear();

        foreach(InventorySlot slot in slots)
        {
            if(slot.item != null)
            {
                items.Add(slot.item);
            }
        }
    }
}
