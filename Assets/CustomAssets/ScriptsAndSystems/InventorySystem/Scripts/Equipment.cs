using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Hammer, Sword}
public enum EquipmentSlot { Head, Chest, Legs, Feets, RightHand, LeftHand}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;

    public int armorModifier;
    public int damageModifier;




    [Header("Юзать онли на оружии правой руки!")]
    [Tooltip("Юзать онли на оружии правой руки!")]
    public bool RequireTwoSlots = false;

    public WeaponType weaponType;

    [Header("Skills")]
    [Tooltip("Указывать по одному на одноручку и бронь(chest и legs) и 2 на двуручку! Возможно отсутствие скилла")]
    public Skill SlotWeapon1;
    public bool useSlotWeapon1Spell = false;
    [Tooltip("Указывать по одному на одноручку и бронь(chest и legs) и 2 на двуручку! Возможно отсутствие скилла")]
    public Skill SlotWeapon2;
    public bool useSlotWeapon2Spell = false;
    [Tooltip("Указывать по одному на одноручку и бронь(chest и legs) и 2 на двуручку! Возможно отсутствие скилла")]
    public Skill SlotChest;
    public bool useSlotChestSpell = false;
    [Tooltip("Указывать по одному на одноручку и бронь(chest и legs) и 2 на двуручку! Возможно отсутствие скилла")]
    public Skill SlotLegs;
    public bool useSlotLegsSpell = false;

    public override void Use(ItemInstance instance)
    {
        base.Use(instance);
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public override void RemoveFromInventory()
    {
        InventoryManager.instance.RemoveItem(this);
    }
}

