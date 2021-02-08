using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "A name of an item";
    public Sprite icon; //Inventory icon
    public bool isDefaultItem = false;
    public string description = "Item description";
    public bool stackable = false;
    
    public GameObject dropPrefub;
    
    
    public int MaxCountPerStack = 100;



    public virtual void Use(ItemInstance instance)
    {
        Debug.Log("Item " + name + " used");
    }
    public virtual void RemoveFromInventory()
    {
        /*Оверрайд для стакаблов*/
    }
}

// A class that holds a real instance of a ScriptableObject item.
// Allows us to have copies with mutable data.
[System.Serializable]
public class ItemInstance
{
    // Reference to scriptable object "template".
    public Item item;
    // Object-specific data.
    private int count;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if (count <= 0)
            {
                InventoryManager.instance.RemoveItem(this.item);
            }
        }
    }

    public ItemInstance(Item item, int count)
    {
        this.item = item;
        this.Count = count;
    }
}
