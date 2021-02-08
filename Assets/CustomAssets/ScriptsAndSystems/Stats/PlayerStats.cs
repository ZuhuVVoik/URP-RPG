using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModfier(newItem.armorModifier);
            damage.AddModfier(newItem.damageModifier);
        }


        if(oldItem != null)
        {
            armor.RemoveModfier(oldItem.armorModifier);
            damage.RemoveModfier(oldItem.damageModifier);
        }
    }
}
