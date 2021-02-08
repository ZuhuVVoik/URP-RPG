using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemAmount item;

    public InventoryTooltip tooltip;


    public Image image;
    public Text count;

    private void Start()
    {
        tooltip = FindObjectOfType<InventoryTooltip>();
    }

    public void UpdateItem(ItemAmount itemAmount)
    {
        item = itemAmount;
        image.sprite = itemAmount.Item.icon;
        count.text = itemAmount.Amount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item.Item != null)
        {
            tooltip.GenerateTooltip(item.Item);
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
}
