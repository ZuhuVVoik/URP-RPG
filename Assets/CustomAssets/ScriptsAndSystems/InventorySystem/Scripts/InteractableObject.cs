using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Interactable
{
    new public string name;

    public ProgressBar progressBar;
    public InventoryUI bindedUI;
    //  Binded UI по статикам под тип объекта
    public bool requirePlayerInventory;
    public Inventory localInventory;

    public bool canBeRemovedByPlayer = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndInterraction();
        }
    }

    public override void InterractAlternatively()
    {
        base.InterractAlternatively();
    }

    public override void Interract()
    {
        base.Interract();

        InventoryManager.instance.player.OnInterraction(this);

        StartInterraction();
    }

    public virtual void OnCreation()
    {
        
    }

    public virtual void StartInterraction()
    {
        InventoryManager.instance.OpenLocalInventory(this);
    }
    public virtual void EndInterraction()
    {
        InventoryManager.instance.CloseLocalInventory(this);
    }

    public virtual void SaveItems(InventorySlot[] slots)
    {
        localInventory.SaveItems(slots);
    }
}
