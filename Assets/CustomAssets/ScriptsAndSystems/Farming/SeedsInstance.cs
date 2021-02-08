using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsInstance : MonoBehaviour
{
    public SeedsCulture seeds;
    public float currentGrowth = 0f;
    public float growthStrength;
    public GameObject currentPlantPrefab;
    public int currentPrefubIndex;
    public int prefabCount;
    public float timeToSwtichPrefabs;

    public void CreateInstance(SeedsCulture seeds)
    {
        this.seeds = seeds;

        currentPlantPrefab = seeds.growthPrefabs[0];
        growthStrength = 1 / seeds.growthTime;
        prefabCount = seeds.growthPrefabs.Count;
        currentPrefubIndex = 0;
        timeToSwtichPrefabs = prefabCount / seeds.growthTime;
    }

    public bool CanGetLoot()
    {
        if (currentGrowth >= 1f)
            return true;
        return false;
    }
    public List<ItemInstance> GetLoot()
    {
        return seeds.GetLoot();
    }

}
