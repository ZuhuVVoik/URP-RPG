using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemInstance item;
    public int count;

    public override void Interract()
    {
        base.Interract();

        PickUp();
    }

    public void PickUp()
    {
        Debug.Log("Item " + item.item.name + " picked up");

        item.Count = count;


        bool pickedUp = InventoryManager.instance.AddItem(item);

        if (pickedUp)
        {
            Destroy(gameObject);
        }
    }
    
    
}
