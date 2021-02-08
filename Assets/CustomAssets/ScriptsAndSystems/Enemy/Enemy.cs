using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(LootTable))]
public class Enemy : MonoBehaviour
{
    EnemyController controller;
    EnemyStats stats;
    LootTable lootTable;
    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        lootTable = GetComponent<LootTable>();
        controller = GetComponent<EnemyController>();
    }

    public bool isDead = false;
    public void OnDeath()
    {
        controller.OnDeath();
        isDead = true;
    }
}
