using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkillBase : MonoBehaviour
{
    protected Skill skill;
    protected VisualEffect visuals;

    private void Start()
    {
        SkillInstancedata data = GetComponent<SkillInstancedata>();
        skill = data.skill;

        SkillInstanceMain instanceMain = this.GetComponent<SkillInstanceMain>();
        
        visuals = instanceMain.vfx;        

        StartVisuals();

        StartPreDamageDelay();
    }
    public void DeactivateVisuals()
    {
        visuals.gameObject.SetActive(false);
    }
    public virtual void StartVisuals()
    {
        Debug.Log("Skill " + skill.name + " started to draw visuals");

        visuals.gameObject.SetActive(true);
        visuals.Play();
    }

    public virtual void StartAction()
    {
        Debug.Log("Skill " + skill.name + " started todo something");
        
    }

    public void StartPreDamageDelay()
    {
        if(skill.timeBeforeDamage > 0)
        {
            StartCoroutine(PreDamageDelay());
        }
        else
        {
            StartAction();
        }
    }

    IEnumerator PreDamageDelay()
    {
        yield return new WaitForSeconds(skill.timeBeforeDamage);
        StartAction();
    }
}
