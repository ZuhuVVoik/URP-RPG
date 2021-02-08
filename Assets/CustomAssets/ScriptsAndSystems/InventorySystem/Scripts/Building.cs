using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Building")]
public class Building : Consumable
{
    [SerializeField]
    public GameObject BuildingPrefab;
    [SerializeField]
    public GameObject BuildingPreview;
    public int gold;

    BuildingProccess player;
    
    public override void Use(ItemInstance instance)
    {
        Debug.Log("Placing building " + this.name);
        player = GameObject.Find("Player").GetComponent<BuildingProccess>();
        player.OnBuildingProccessStart(this, instance);
    }

    

}

