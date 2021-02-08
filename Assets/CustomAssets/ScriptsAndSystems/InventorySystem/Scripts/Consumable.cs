using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public override void Use(ItemInstance instance)
    {
        base.Use(instance);
        Debug.Log("Used " + this.name + " as consumable item");

        if (stackable)
        {
            instance.Count -= 1;
        }
    }

    public override void RemoveFromInventory()
    {
        InventoryManager.instance.RemoveItem(this);
    }
}
