using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAOEScript : SkillBase
{
    bool lerpEnd;

    public override void StartAction()
    {
        base.StartAction();

        CastSphere(); 
    }
    public override void StartVisuals()
    {
        base.StartVisuals();

    }


    private void Update()
    {

    }


    private void CastSphere()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, skill.baseRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.layer == 10) // Damagable layer
            {
                CharacterStats characterStats = collider.gameObject.GetComponent<EnemyStats>() as CharacterStats;
                characterStats.TakeDamage(5);
                Debug.Log("Character " + characterStats.gameObject.name + " has taken damage ");
            }
        }

        DeactivateVisuals();
        Destroy(gameObject);
    }
}
