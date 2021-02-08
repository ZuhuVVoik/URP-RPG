using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform invParent;

    public GameObject inventoryUI;
    public PlayerInventory playerInventory;
    public ChestUI chestUI;
    public LootUI lootUI;
    public CraftUI craftUI;
    public OpenCraftWindow CraftButton;
    public FarmSpotUI farmSpotUI;
    public ProgressBar progressBar;
    public DivideItemUI divideItemUI;

    public InventoryManager InventoryManager;

    protected InventorySlot[] slots;



    // Start is called before the first frame update
    void Start()
    {
        InventoryManager = InventoryManager.instance;
        InventoryManager.onItemChangedCallback += UpdateUI;

        slots = invParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            CraftButton.gameObject.SetActive(!CraftButton.gameObject.activeSelf);


        }
    }

    public virtual void UpdateUI()
    {
        GetSlots();

        for (int i = 0; i < slots.Length; i++)
        {
            if(i < InventoryManager.items.Count)
            {
                slots[i].AddItem(InventoryManager.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public virtual void ClearUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }

    public virtual InventorySlot[] GetSlots()
    {
        return slots;
    }
}
