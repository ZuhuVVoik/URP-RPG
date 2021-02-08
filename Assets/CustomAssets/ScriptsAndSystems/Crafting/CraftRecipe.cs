using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
	public Item Item;
	[Range(1, 999)]
	public int Amount;
}


[CreateAssetMenu(fileName = "New Craft Recipe", menuName = "Recipes/Craft Recipe (ScriptableObject)")]
public class CraftRecipe : ScriptableObject
{
	[Tooltip("Не более 3")]
	public List<ItemAmount> Materials;
	[Tooltip("Не более 2")]
	public List<ItemAmount> Results;


	public InventoryManager inventory;



    public bool CanCraft()
    {
		inventory = InventoryManager.instance;
		return inventory.HaveSpace() && HaveMaterials();
    }

	public bool HaveMaterials()
    {
		List<ItemInstance> inventoryItems = inventory.items;
		foreach(ItemAmount itemAmount in Materials)
        {
			if(inventory.ItemCount(itemAmount.Item) < itemAmount.Amount)
            {
				return false;
            }
        }

		return true;
    }

	public void Craft()
	{
		if (CanCraft())
		{
			RemoveMaterials();
			AddResults();
		}
	}

    private void AddResults()
    {
		foreach (ItemAmount itemAmount in Results)
		{
			for (int i = 0; i < itemAmount.Amount; i++)
			{
				inventory.AddItem(new ItemInstance(itemAmount.Item,itemAmount.Amount));
			}
		}
	}

    private void RemoveMaterials()
    {
		foreach (ItemAmount itemAmount in Materials)
		{
			for (int i = 0; i < itemAmount.Amount; i++)
			{
				inventory.RemoveItem(itemAmount.Item);				
			}
		}
	}
}
