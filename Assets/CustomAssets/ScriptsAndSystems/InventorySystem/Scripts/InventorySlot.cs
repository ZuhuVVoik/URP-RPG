using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, 
    IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image icon;
    public Button removeButton;
    [SerializeField]
    public InventoryTooltip tooltip;
    [SerializeField]
    public DragableSlot dragItem;
    public PlayerMotor player;

    public Text count;

    public bool isInPlayerInventory = false;
    public bool trackItem = false;

    public ItemInstance trackingItem;
    public ItemInstance item;

    public void Update()
    {

    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMotor>();
    }

    

    public void RefreshSlot(ItemInstance newItem)
    {
        if (newItem.Count > 0)
        {
            icon.enabled = true;
            icon.sprite = newItem.item.icon;
            removeButton.interactable = true;
            if (item.item.stackable)
            {
                count.text = item.Count.ToString();
            }
            else
            {
                count.text = "";
            }
        }
        else
        {
            ClearSlot();
        }
    }

    public void AddItem(ItemInstance item)
    {
        this.item = item;
        this.icon.enabled = true;
        this.icon.sprite = item.item.icon;
        this.removeButton.interactable = true;
        if (item.item.stackable)
        {
            count.text = item.Count.ToString();
        }
        else
        {
            count.text = "";
        }
    }

    public void ClearSlot()
    {
        this.item = null;
        count.text = "";
        this.icon.sprite = null;
        this.icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

            dragItem.OnDragStart(this);

    }

    public void OnDrag(PointerEventData eventData)
    {

            dragItem.OnDrag();
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventorySlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InventorySlot>(); //Проверка чтобы именно в ячейку класть

        if(slot.item != null)
        {
            if (slot.item.item == this.item.item && item.item.stackable)
            {
                if (item.item.MaxCountPerStack <= (item.Count + slot.item.Count))
                {
                    int difference = item.item.MaxCountPerStack - item.Count;
                    slot.item.Count += difference;
                    this.item.Count -= difference;

                    if (this.item != null)
                    {
                        this.RefreshSlot(this.item);
                    }

                    slot.RefreshSlot(slot.item);
                    this.RefreshSlot(this.item);
                }
                else
                {
                    slot.item.Count += this.item.Count;

                    slot.RefreshSlot(slot.item);
                    if (this.isInPlayerInventory)
                    {
                        InventoryManager.instance.items.Remove(this.item);
                    }

                    this.ClearSlot();
                }
            }

            if (slot.item != null && this.item != null)
            {
                if (slot.item != this.item || !item.item.stackable)
                {
                    ItemInstance temp = new ItemInstance(null, 0);
                    temp = this.item;
                    this.item = slot.item;
                    slot.item = temp;

                    slot.RefreshSlot(slot.item);
                    this.RefreshSlot(slot.item);
                }
            }

        }
        else if (slot.item == null)
        {
            slot.item = this.item;

            slot.RefreshSlot(slot.item);
            if (this.isInPlayerInventory)
            {
                InventoryManager.instance.items.Remove(this.item);
            }
            this.ClearSlot();
        }


        dragItem.OnDragEnd();
        if (trackItem)
        {
            trackingItem = slot.item;
        }
    }

    public ItemInstance ReturnItem()
    {
        return trackingItem;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.item != null)
        {
            tooltip.GenerateTooltip(item.item);
        }
        else
        {
            tooltip.gameObject.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }







    public void OnRemoveButton()
    {
        GameObject removeObj = Instantiate(item.item.dropPrefub, player.transform.position, player.transform.rotation);
        ItemPickUp removeObjPickup = removeObj.GetComponent<ItemPickUp>();
        Debug.Log(removeObjPickup);
        removeObjPickup.count = this.item.Count;
        removeObjPickup.item = this.item;
        InventoryManager.instance.RemoveItem(item.item);
    }

    public void useItem()
    {
        if (item != null)
        {
            item.item.Use(item);

            RefreshSlot(item);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            if (item.item.stackable && isInPlayerInventory)
            {
                if(item.Count > 1)
                {
                    InventoryManager.instance.InventoryUI.divideItemUI.Activate(this.item, this);
                }
            }
        }
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            this.useItem();
        }
    }
}
