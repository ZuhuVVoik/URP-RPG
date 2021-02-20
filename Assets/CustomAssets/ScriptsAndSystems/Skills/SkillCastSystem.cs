using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCastSystem : MonoBehaviour
{
    public SkillUI skillUI;

    public List<Skill> currentSkills;
    List<bool> cooldowns = new List<bool>();
    Skill currentSkill;
    Camera cam;
    RaycastHit hit;
    GameObject flyingSkill;
    private Vector3 currentpos;
    public GameObject currentpreview;

    bool isPreviewActive = false;



    private void Start()
    {
        skillUI = FindObjectOfType<SkillUI>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Skill1") && !cooldowns[0])
        {
            ChooseSkillForPreview(0);
        }
        if (Input.GetButtonDown("Skill2") && !cooldowns[1])
        {
            ChooseSkillForPreview(1);
        }
        if (Input.GetButtonDown("Skill3") && !cooldowns[2])
        {
            ChooseSkillForPreview(2);
        }
        if (Input.GetButtonDown("Skill4") && !cooldowns[3])
        {
            ChooseSkillForPreview(3);
        }





        if (isPreviewActive)
        {
            if (isPreviewActive)
                startPreview(currentSkill);


            if (Input.GetButtonDown("Fire1"))
            {
                Cast(currentSkill ,flyingSkill.transform);

                int index = currentSkills.IndexOf(currentSkill);
                SkillCoolDown(index, currentSkill.coolDown);
                
                StopPreview();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                StopPreview();
            }
        }
    }


    public void SkillCoolDown(int skillIndex, float time)
    {
        cooldowns[skillIndex] = true;
        skillUI.DrawCoolDownAnimation(skillIndex);
        StartCoroutine(Cooldown(skillIndex, time));

    }
    IEnumerator Cooldown(int skillindex, float time)
    {
        yield return new WaitForSeconds(time);
        cooldowns[skillindex] = false;
    }

    public void RefreshSkills()
    {
        currentSkills.Clear();
        cooldowns = new List<bool>() { false,false,false,false };

        currentSkills.Add(SkillsManager.instance.SlotWeapon1);
        currentSkills.Add(SkillsManager.instance.SlotWeapon2);
        currentSkills.Add(SkillsManager.instance.SlotChest);
        currentSkills.Add(SkillsManager.instance.SlotLegs);

   

        /*Получим данные о кулдаунах, перезапустим их и пусть потом убираются*/
        for (int i = 0; i < currentSkills.Count; i++)
        {
            if (currentSkills[i] != null)
            {
                cooldowns.Add(true);
                SkillCoolDown(i, currentSkills[i].coolDown);
            }
        }

    }



    private void ShowSkillPreview(Skill skill)
    {
        skill.PrepareSkill();
        isPreviewActive = true;
    }
    private void UseSkill(Skill skill)
    {
        skill.UseSkill(this);
    }
    private void StopPreviewSkill(Skill skill)
    {
        skill.DenySkill();
    }
    private void DenySkill()
    {

    }




    public void startPreview(Skill skill)
    {
        if (Physics.Raycast(new Vector3(cam.transform.position.x + 1, cam.transform.position.y, cam.transform.position.z), cam.transform.forward, out hit, skill.maxCastDistance))
        {
                if(flyingSkill == null)
                {
                    flyingSkill = Instantiate(currentpreview);
                }
                showPreview(hit, skill);
            
        }
    }

    public void showPreview(RaycastHit hit2, Skill skill)
    {
        currentpos = hit2.point;

        //currentpos = new Vector3(Mathf.Round(currentpos.x), Mathf.Round(currentpos.y), Mathf.Round(currentpos.z));
        currentpos = new Vector3(currentpos.x, currentpos.y, currentpos.z);

        currentpreview.transform.position = currentpos;

        flyingSkill.transform.position = Vector3.Lerp(flyingSkill.transform.position, currentpos, 10f);
    }

    public void StopPreview()
    {
        currentSkill = null;
        Destroy(flyingSkill);

        isPreviewActive = false;
    }
    public void ChooseSkillForPreview(int indexInArray)
    {
        currentSkill = currentSkills[indexInArray];
        currentpreview = currentSkills[indexInArray].skillPreview;
        isPreviewActive = true;
    }
    public void Cast(Skill skill, Transform position)
    {

        GameObject skillObj = Instantiate(skill.skillVisual, position.position, position.rotation);
        SkillInstancedata data = skillObj.GetComponent<SkillInstancedata>();

        data.caster = this.gameObject;
        data.skill = skill;
    }
}