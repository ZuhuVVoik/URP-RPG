using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Farming Culture", menuName = "Farming/Plants")]
public class SeedsCulture : Consumable
{
    [Header("Для растений")]
    public float growthTime;
    public List<GameObject> growthPrefabs;

    public Loot[] loots;

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

    public SeedsInstance CreateInstance()
    {
        return new SeedsInstance() { seeds = this };
    }
}
