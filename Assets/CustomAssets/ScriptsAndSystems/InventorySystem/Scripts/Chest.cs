using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    public PlayerMotor player;
    public InventoryManager inventoryManager;

    public GameObject DropPrefab;

    private void Start()
    {
        player = FindObjectOfType<PlayerMotor>();
        inventoryManager = InventoryManager.instance;

        localInventory = GetComponent<Inventory>();

        bindedUI = inventoryManager.InventoryUI.chestUI;
    }

    public override void Interract()
    {
        base.Interract();

        inventoryManager.OpenLocalInventory(this);
    }


    public override void StartInterraction()
    {
        base.StartInterraction();
    }


    public override void InterractAlternatively()
    {
        Inventory inventory = GetComponent<Inventory>();
        if (inventory.items.Count == 0)
        {
            GameObject obj = this.gameObject;

            GameObject gameObject = Instantiate(DropPrefab, player.transform.position, player.transform.rotation);

            Destroy(obj);
        }
    }
}
