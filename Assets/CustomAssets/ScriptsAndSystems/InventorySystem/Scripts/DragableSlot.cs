using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragableSlot : MonoBehaviour
{
    public Image icon;
    ItemInstance currentItem;

    private void Start()
    {
        
    }

    public void OnDragStart(InventorySlot slot)
    {
        if(slot.item != null)
        {
            this.gameObject.SetActive(true);
            currentItem = slot.item;
            icon.sprite = slot.icon.sprite;
            UpdateDragPreview();
        }
    }

    public void OnDragEnd()
    {
        UpdateDragPreview();
        this.gameObject.SetActive(false);
    }

    public void OnDrag()
    {
        transform.position = Input.mousePosition;
    }

    private void Update()
    {
        //Debug.Log(currentItem.icon);
    }

    public void UpdateDragPreview()
    {
        /*Тут вписывать обновляемую инфу типа пикч, количества и тд*/
        icon.sprite = currentItem.item.icon;
    }
}
