using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FarmSpotUI : MonoBehaviour
{
    public InventorySlot plantSlot;
    public Image lastGrown;
    public CloseUIButton closeButton;
    public Button getLootButton;
    public List<Image> LootImages;

    public FarmingSpot currentSpot;

    private void Start()
    {
        HideResultList();
        getLootButton.gameObject.SetActive(false);
        lastGrown.gameObject.SetActive(false);
    }
    private void Update()
    {
        InteractableObject obj = InventoryManager.instance.currentInteractableObject;
        FarmingSpot spot = obj.GetComponent<FarmingSpot>();
        if(spot != null && plantSlot.item != null)
        {
            spot.StartGrow();
            //                                                                                      ||
            //                                                                                     \||/
            //Не считаю систему с 4 семечками из крутой, н очуть что тут помно поменять значыение   \/
            if(plantSlot.item.Count > 1)
            {
                ItemInstance itemInstance = new ItemInstance(plantSlot.item.item, plantSlot.item.Count - 1);
                InventoryManager.instance.AddItem(itemInstance);
            }
            plantSlot.item = null;
        }
    }
    public void CheckState(FarmingSpot spot)
    {
        if(spot == currentSpot)
        {
            if (spot.readyToBegin && !spot.growthStarted && !spot.growthEnded)
            {
                if (spot.lastGrown != null)
                {
                    UpdateLastGrown(spot);
                }

                HideResultList();
                plantSlot.gameObject.SetActive(true);
                getLootButton.gameObject.SetActive(false);
            }
            if (spot.growthStarted && !spot.growthEnded && !spot.readyToBegin)
            {
                HideResultList();
                lastGrown.gameObject.SetActive(true);
                plantSlot.gameObject.SetActive(false);
                getLootButton.gameObject.SetActive(false);
            }
            if (spot.growthEnded && !spot.growthStarted && !spot.readyToBegin)
            {
                ShowResultList(spot.loot);
                plantSlot.gameObject.SetActive(false);
                lastGrown.gameObject.SetActive(false);
                getLootButton.gameObject.SetActive(true);
            }
        }
    }
    public void ShowResultList(List<ItemInstance> items)
    {
        for(int i = 0; i < items.Count; i++)
        {
            LootImages[i].gameObject.SetActive(true);
            LootImages[i].sprite = items[i].item.icon;
        }

        //for(int i = 0; i < LootImages.Count; i++)
        //{
        //    if(items[i] != null)
        //    {
        //        LootImages[i].gameObject.SetActive(true);
        //        LootImages[i].sprite = items[i].item.icon;
        //    }
        //}
    }

    public void HideResultList()
    {
        for (int i = 0; i < LootImages.Count; i++)
        {
            LootImages[i].sprite = null;
            LootImages[i].gameObject.SetActive(false);
        }
    }

    public void UpdatePrev(SeedsInstance currentSeedInstance)
    {
        lastGrown.sprite = currentSeedInstance.seeds.icon;
    }

    public void OnGetLootClick()
    {
        InteractableObject obj = InventoryManager.instance.currentInteractableObject;
        FarmingSpot spot = obj.GetComponent<FarmingSpot>();

        spot.TakeLoot();

        this.gameObject.SetActive(false);
    }

    public void UpdateLastGrown(FarmingSpot spot)
    {
        lastGrown.gameObject.SetActive(true);
        lastGrown.sprite = spot.lastGrown;
    }
}
