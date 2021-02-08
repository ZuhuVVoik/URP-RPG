using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }


    InventoryManager inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged; //коллбек на изменееик предмета

    public Equipment[] currentEquipment;

    private void Start()
    {
        inventory = InventoryManager.instance;

        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
    }

    public List<Skill> GetSpellsList()
    {
        List<Skill> skills = new List<Skill>();

        if (currentEquipment[4] != null)
        {
            if (currentEquipment[4].useSlotWeapon1Spell)
                skills.Add(currentEquipment[4].SlotWeapon1);
            else
                skills.Add(null);
        }
        else
        {
            skills.Add(null);
        }
        if (currentEquipment[5] != null)
        {
            if (currentEquipment[5].useSlotWeapon2Spell)
                skills.Add(currentEquipment[5].SlotWeapon2);
            else
                skills.Add(null);
        }
        else
        {
            skills.Add(null);
        }
        if (currentEquipment[1] != null)
        {
            if (currentEquipment[1].useSlotChestSpell)
                skills.Add(currentEquipment[1].SlotChest);
            else
                skills.Add(null);
        }
        else
        {
            skills.Add(null);
        }
        if (currentEquipment[2] != null)
        {
            if (currentEquipment[2].useSlotLegsSpell)
                skills.Add(currentEquipment[2].SlotLegs);
            else
                skills.Add(null);
        }
        else
        {
            skills.Add(null);
        }

        return skills;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot; //Возьмем индекс ячейки предмета

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null) //Если что-то экипировано
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(new ItemInstance(oldItem,1));
        }

        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);

        currentEquipment[slotIndex] = newItem; // Поместим предмет в нужное место

        RefreshSkillsList();
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            inventory.AddItem(new ItemInstance(currentEquipment[slotIndex],1));
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, currentEquipment[slotIndex]); // нулл тк нету предмета
        }
        RefreshSkillsList();
    }
    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        RefreshSkillsList();
    }

    public void RefreshSkillsList()
    {
        SkillsManager.instance.RefreshSkills();
    }
}
