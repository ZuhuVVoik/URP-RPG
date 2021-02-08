using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InstrumentType { Axe, Pickaxe, Hoe}
[CreateAssetMenu(fileName = "New Instrument", menuName = "Inventory/Instrument")]
public class Instrument : Item
{
    public InstrumentType instrumentType;

    public int Tier = 1;
    
    public int maxDurability = 100;
    [Tooltip("Ставить таким же как Max Durability, иначе будет поломанным, ниже 0 не ставить")]
    public int currentDurability = 100;

    private bool canBeUsed = true;
    

    public void Repair(int repairScores)
    {
        currentDurability += repairScores;
        if(currentDurability > maxDurability)
        {
            currentDurability = maxDurability;
        }

        if(currentDurability >= 0)
        {
            canBeUsed = true;
        }
    }
    public void TakeDamage(int damageScores)
    {
        currentDurability -= damageScores;
        if(currentDurability <= 0)
        {
            currentDurability = 0;
            canBeUsed = false;
        }
    }
}
