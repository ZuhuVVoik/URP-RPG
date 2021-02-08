using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public SkillUIIcon[] icons;
    public Sprite NoSkillPlaceHolder;


    public void RefreshSkillIcons(List<Skill> skills)
    {
        for(int i = 0; i < 4; i++)
        {
            if(i <= skills.Count)
            {
                if (skills[i] != null)
                {
                    icons[i].image.sprite = skills[i].icon;
                    icons[i].cooldownTime = skills[i].coolDown;
                    icons[i].skill = skills[i];
                }
                else
                {
                    icons[i].image.sprite = NoSkillPlaceHolder;
                    icons[i].cooldownTime = 0.5f;
                }
            }
            else
            {
                icons[i].image.sprite = NoSkillPlaceHolder;
                icons[i].cooldownTime = 0.5f;
            }
        }
    }

    private void Update()
    {
        DoCoolDownAnimation();
    }

    public void DoCoolDownAnimation()
    {
        foreach(SkillUIIcon skill in icons)
        {
            if(skill.skill != null)
            {
                if (skill.cooldownImage.fillAmount > 0f)
                {
                    skill.cooldownImage.fillAmount -= 1f / skill.skill.coolDown * Time.deltaTime;
                }
                else
                {
                    skill.cooldownImage.fillAmount = 0f;
                }
            }
        }
    }
    public void DrawCoolDownAnimation(int spellIndex)
    {
        icons[spellIndex].cooldownImage.fillAmount = 1f;
    }
}
