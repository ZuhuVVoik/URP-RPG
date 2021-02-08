using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LootNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryTooltip tooltip;
    public LootUI lootUI;

    public ItemInstance itemInstance;
    private Item item;

    public Image image;
    public Text text;
    public Text quantity;

    private void Start()
    {

    }

    public void RefreshNode(ItemInstance item)
    {
        itemInstance = item;
        this.item = itemInstance.item;

        image.sprite = this.item.icon;
        text.text = this.item.name;
        quantity.text = this.itemInstance.Count.ToString();
    }

    public void ClearNode()
    {
        itemInstance = null;
        item = null;
        image.sprite = null;
        text.text = null;
        quantity.text = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Looted " + quantity + "*" + item.name);
        lootUI.Loot(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.GenerateTooltip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
