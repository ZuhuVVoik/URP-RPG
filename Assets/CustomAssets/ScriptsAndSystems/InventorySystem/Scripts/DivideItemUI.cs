using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideItemUI : MonoBehaviour
{
    public Text currentCount;
    public Text maxCount;
    public Slider slider;

    private ItemInstance currentInastance;
    private InventorySlot currentSlot;
    public void Activate(ItemInstance itemInstance, InventorySlot slot)
    {
        this.gameObject.SetActive(true);
        currentInastance = itemInstance;
        slider.maxValue = itemInstance.Count - 1;
        slider.value = Mathf.RoundToInt(itemInstance.Count / 2);
        maxCount.text = (itemInstance.Count).ToString();
        currentCount.text = slider.value.ToString();
        currentSlot = slot;
    }

    public void Divide()
    {
        int newSlotCount = Mathf.RoundToInt(slider.value);
        ItemInstance newItemInstance = new ItemInstance(currentInastance.item, newSlotCount);
        InventoryManager.instance.AddItem(newItemInstance);

        currentInastance.Count -= newSlotCount;
        currentSlot.RefreshSlot(currentInastance);

        this.gameObject.SetActive(false);
    }

    public void ChangeCurrentValue()
    {
        currentCount.text = slider.value.ToString();
    }
}
