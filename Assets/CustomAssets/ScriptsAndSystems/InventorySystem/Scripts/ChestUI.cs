using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : InventoryUI
{


    public override void UpdateUI()
    {
        slots = GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < InventoryManager.instance.localInventory.items.Count)
            {
                slots[i].AddItem(InventoryManager.instance.localInventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public override void ClearUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }


}
