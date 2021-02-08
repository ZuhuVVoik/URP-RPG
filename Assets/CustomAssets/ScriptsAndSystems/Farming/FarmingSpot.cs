using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmingSpot : InteractableObject
{
    public FarmSpotUI farmSpotUI;
    public SeedsInstance currentSeedInstance;

    public List<ItemInstance> loot;
    public List<GameObject> prefabPlaces;
    private List<GameObject> currentPrefabs;
    public bool growthStarted = false;
    public bool growthEnded = false;
    public bool readyToBegin = true;

    public Sprite lastGrown;

    private bool interracting = false;

    private void Start()
    {
        currentSeedInstance = GetComponent<SeedsInstance>();

        farmSpotUI = InventoryManager.instance.InventoryUI.farmSpotUI;
        bindedUI = InventoryManager.instance.InventoryUI;
    }
    public void TakeLoot()
    {

        growthStarted = false;
        growthEnded = false;
        readyToBegin = true;

        for(int i = 0; i < prefabPlaces.Count; i++)
        {
            GameObject obj = currentPrefabs[0];

            currentPrefabs.Remove(obj);
            Destroy(obj);
        }

        currentSeedInstance = null;

        foreach (ItemInstance item in loot)
        {
            InventoryManager.instance.AddItem(item);
        }


        loot.Clear();
    }

    private void Update()
    {
        if (currentSeedInstance != null)
        {
            if (currentSeedInstance.CanGetLoot())
            {
                OnGrowthEnds();
            }

            if (growthStarted && !growthEnded)
            {
                InvokeRepeating("UpdateGrowth", 1f, 1f);
            }

            if (interracting)
            {
                farmSpotUI.CheckState(this);
            }
            if (farmSpotUI.gameObject.active)
            {
                EndInterraction();
            }

            farmSpotUI.CheckState(this);
        }
    }


    public void StartGrow()
    {
        farmSpotUI.lastGrown.gameObject.SetActive(false);

        SeedsCulture seeds = farmSpotUI.plantSlot.item.item as SeedsCulture;
        if(seeds != null)
        {
            this.currentSeedInstance.CreateInstance(seeds);

            growthStarted = true;
            growthEnded = false;
            readyToBegin = false;
        }
    }
    public void OnGrowthEnds()
    {
        growthStarted = false;
        growthEnded = true;
        readyToBegin = false;

        lastGrown = currentSeedInstance.seeds.icon;

        loot = currentSeedInstance.GetLoot();
    }

    public override void Interract()
    {
        base.Interract();

        interracting = true;
        farmSpotUI.gameObject.SetActive(true);
        farmSpotUI.currentSpot = this;
        farmSpotUI.CheckState(this);
    }
    public override void EndInterraction()
    {
        interracting = false;
    }

    public void ReplacePrefabs()
    {
        currentPrefabs = new List<GameObject>();
        foreach(GameObject place in prefabPlaces)
        {
            GameObject obj = Instantiate(currentSeedInstance.currentPlantPrefab, place.transform.position, place.transform.rotation, place.transform);
            currentPrefabs.Add(obj);
        }
    }

    public void UpdateGrowth()
    {
        if(currentSeedInstance.currentGrowth < 1)
        {
            if(currentSeedInstance.currentPrefubIndex < currentSeedInstance.prefabCount)
            {
                currentSeedInstance.currentPrefubIndex++;
                currentSeedInstance.currentPlantPrefab = currentSeedInstance.seeds.growthPrefabs[currentSeedInstance.currentPrefubIndex];
                ReplacePrefabs();
            }
        }
        currentSeedInstance.currentGrowth += currentSeedInstance.growthStrength * Time.deltaTime;
    }
}
