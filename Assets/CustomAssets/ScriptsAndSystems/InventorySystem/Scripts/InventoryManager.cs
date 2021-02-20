using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /* Этот класс под единый инвентарь игрока */
    /*  */

    #region Singleton
    public static InventoryManager instance;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    public AgentMovement player;
    public PlayerInventory playerInventory;
    public Inventory localInventory;

    public List<ItemInstance> items = new List<ItemInstance>();
    public int space = 20;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public InventoryUI InventoryUI;

    public InteractableObject currentInteractableObject;

    public bool AddItem(ItemInstance item)
    {
        if (!item.item.isDefaultItem)
        {
            if(this.items.Count <= space)
            {
                this.items.Add(item);

                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }

                return true;
            }
            else
            {
                Debug.Log("Inventory is full");
                return false;
            }
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(items.Find(t => t.item == item));

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }


    public void OpenLocalInventory(InteractableObject interactableObject)
    {
        currentInteractableObject = interactableObject;



        if (currentInteractableObject.requirePlayerInventory)
        {
            playerInventory.gameObject.SetActive(true);   
        }

        currentInteractableObject.bindedUI.gameObject.SetActive(true);

        localInventory = currentInteractableObject.localInventory;

        currentInteractableObject.bindedUI.UpdateUI();
    }
    public void CloseLocalInventory(InteractableObject interactableObject)
    {
        if(currentInteractableObject == interactableObject)
        {
            currentInteractableObject.SaveItems(currentInteractableObject.bindedUI.GetSlots());
            currentInteractableObject.bindedUI.ClearUI();
            currentInteractableObject.bindedUI.gameObject.SetActive(false);

            playerInventory.gameObject.SetActive(false);
        }
    }

    public int ItemCount(Item item)
    {
        foreach(ItemInstance itemInstance in items)
        {
            if (item == itemInstance.item)
                return itemInstance.Count;
        }

        return 0;
    }

    public bool HaveSpace()
    {
        if (this.items.Count <= space)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
