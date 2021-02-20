using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillTypes { AttackAoe, Buff}
[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill(Scriptable Object)")]
public class Skill : ScriptableObject
{
    new public string name;
    public Sprite icon;

    public SkillTypes SkillType;

    public int manaCost = 10;

    public float maxCastDistance = 10f;
    public float currentCastDistance = 10f;

    [Header("Настройка AOE")]
    public bool haveAOE = false;
    [Tooltip("Форма области способности")]
    public GameObject skillForm;
    public float baseRadius = 5f;
    [Tooltip("Конечный объект(торнадо, вулкан etc)")]
    public GameObject endObject;

    [Header("На основе волн")]
    public bool haveWaves = false;
    public int baseWaveCount = 1;
    public float timeBetweenWaves = 2f;



    [Header("Базовые значения урона")]
    public float baseDamage = 0f;
    [Tooltip("Падение урона от радиуса, etc")]
    public float damageMin = 0f;
    [Tooltip("Задержка перед стартом")]
    public float timeBeforeDamage = 5f;

    [Tooltip("Нужен ли выбор позиции или применяется просто от позиции игрока")]
    public bool requirePositionChoose = false;
    public bool removeDetailFromTerrain = false;

    [Header("Каст и время")]
    public float coolDown = 1f;
    public bool needToCast = false;
    public float castTime = 1f;
    public float timeToMaxStrength = 1f;

    [Header("Визуал")]
    public GameObject skillPreview;
    public GameObject skillVisual;
    public GameObject skillVFX;
    


    public virtual bool CheckDistance()
    {
        //Не использовать бейс вызов!!
        Debug.Log("Checking distance for " + this.name + " skill");
        return false;
    }
    
    public virtual void PrepareSkill()
    {
        Debug.Log("Skill " + this.name + " is preparing");
    }
    public virtual void DenySkill()
    {
        Debug.Log("Skill " + this.name + " is denied");
    }


    public virtual void UseSkill(SkillCastSystem caster)
    {
        Debug.Log("Skill " + this.name + " used");
    }



}
