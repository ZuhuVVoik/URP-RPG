using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Enemy mainObj;
    private void Start()
    {
        mainObj = GetComponent<Enemy>();
    }
    public override void Die()
    {
        base.Die();

        mainObj.OnDeath();


        //При смерти

    }
}
