using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currenthealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public void Awake()
    {
        currenthealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currenthealth -= damageAmount;
        Debug.Log(this.name + " has taken " + damageAmount);
    
        if(currenthealth <= 0)
        {
            Die();
        }
    }

    public int useDefendStats(int damage)
    {
        //Вписать логику изменения значений урона с учетом показателей защиты
        damage -= armor.GetValue();

        return damage;
    }

    public virtual void Die()
    {
        Debug.Log(this.name + " died");
        //Изменить по разному для ботов и игрока
    }
}
