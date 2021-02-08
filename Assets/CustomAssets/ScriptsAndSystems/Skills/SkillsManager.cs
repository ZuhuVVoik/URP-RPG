using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillSlots { Weapon1, Weapon2, Chest, Legs}
public class SkillsManager : MonoBehaviour
{
    public static SkillsManager instance;
    public SkillCastSystem castSystem;
    public SkillUI skillUI;
    private void Awake()
    {
        instance = this;
        castSystem = FindObjectOfType<SkillCastSystem>();
    }


    [Header("Skills")]
    [SerializeField]
    public Skill SlotWeapon1;
    [SerializeField]
    public Skill SlotWeapon2;
    [SerializeField]
    public Skill SlotChest;
    [SerializeField]
    public Skill SlotLegs;



    public void RefreshSkills()
    {
        List<Skill> skills = EquipmentManager.instance.GetSpellsList();



        if (skills[0] != null)
        {
            SlotWeapon1 = skills[0];
        }
        else
        {

        }
        if (skills[1] != null)
        {
            SlotWeapon2 = skills[1];
        }
        else
        {

        }
        if (skills[2] != null)
        {
            SlotChest = skills[2];
        }
        else
        {

        }
        if (skills[3] != null)
        {
            SlotLegs = skills[3];
        }
        else
        {

        }

        castSystem.RefreshSkills();
        skillUI.RefreshSkillIcons(skills);
    }
}
