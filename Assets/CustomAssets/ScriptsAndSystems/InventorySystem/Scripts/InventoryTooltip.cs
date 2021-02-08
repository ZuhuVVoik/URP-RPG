using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour
{
    public Text itemName;
    public Text itemDescription;

    public void GenerateTooltip(Item item)
    {
        

        itemName.text = item.name;
        itemDescription.text = item.description;

        

        gameObject.SetActive(true);
    }


    private void Start()
    {
        
    }
}
