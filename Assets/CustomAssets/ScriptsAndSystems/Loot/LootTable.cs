using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Item item;
    public float chance;
    public int minQuantity;
    public int maxQuantity;

    public int Quantity { get; set; }
}

public class LootTable : MonoBehaviour
{
    public Lootable obj;
    public Loot[] loots;

    private void Start()
    {
        obj = GetComponent<Lootable>();
    }

    public List<ItemInstance> GetLoot()
    {
        List<ItemInstance> dropList = new List<ItemInstance>();
        float drawn = Random.Range(0f, 100f);

        foreach (Loot loot in loots)
        {
            if (drawn <= loot.chance)
            {
                ItemInstance item = new ItemInstance(loot.item, RandomQuantity(loot));
                dropList.Add(item);
            }
        }

        return dropList;
    }

    public int RandomQuantity(Loot loot)
    {
        return Random.Range(loot.minQuantity, loot.maxQuantity);
    }
}
