using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int standartAttackStrength = 10;
    public float standartAttackCooldown = 1f;

    public float delayBetweenSkills = 2f;
    public float preCastTime = 2f;
    public float playerLostCastRestTime = 3f; // Если игрок потерян, то сколько времени будет сразу возможен каст

    public List<Skill> skills;
    List<bool> skillsCDStatus;

    bool onDelayBetweenCasts = false;
    bool canCastSpells = false;
    bool canAttackStandart = true;
    Coroutine loosePlayerCoroutine;
    PlayerStats player;
    private void Start()
    {
        player = PlayerManager.instance.Player.GetComponent<PlayerStats>();
 

        foreach(Skill skill in skills)
        {
            skillsCDStatus.Add(false);
        }
    }
    
    
    /*  Кусок с автоатаками   */
    public virtual void AttackStandart()
    {
        if (canAttackStandart)
        {
            Debug.Log("Attacking player");
            player.TakeDamage(standartAttackStrength);
            StartCoroutine(AttackStandartDelay());
        }
    }
    IEnumerator AttackStandartDelay()
    {
        canAttackStandart = false;
        yield return new WaitForSeconds(standartAttackCooldown);
        canAttackStandart = true;
    }

    /*  Спеллы  */
    
    /*  Обнаружение игрока */
    public void OnPlayerDetected()
    {
        if (!canCastSpells)
        {
            StartCoroutine(PlayerDetectedCastPrepare());
            if(loosePlayerCoroutine == null)
            {
                StopCoroutine(loosePlayerCoroutine);
            }
        }
    }
    public void OnPlayerLost()
    {
        loosePlayerCoroutine = StartCoroutine(PlayerLost());
    }
    IEnumerator PlayerDetectedCastPrepare()
    {
        yield return new WaitForSeconds(preCastTime);
        canCastSpells = true;
    }
    IEnumerator PlayerLost()
    {
        yield return new WaitForSeconds(playerLostCastRestTime);
        canCastSpells = false;
    }
    IEnumerator DelayBetweenCast()
    {
        onDelayBetweenCasts = false;
        yield return new WaitForSeconds(delayBetweenSkills);
        onDelayBetweenCasts = true;
    }
    
    public bool CastSpell()
    {
        if (canCastSpells)
        {
            if(skills.Count > 0)
            {
                if (!onDelayBetweenCasts)
                {
                    int skillIndex = ChooseSkillToCast();
                    if (skillsCDStatus[skillIndex] == false)
                    {
                        UseSpell(skillIndex);
                        CoolDown(skillIndex);

                        return true;
                    }
                }
            }
        }

        return false;
    }
    public int ChooseSkillToCast()
    {
        return Random.Range(0, skills.Count - 1);
    }
    public void UseSpell(int spellIndex)
    {
        if (skills[spellIndex].haveAOE)
        {
            if (skills[spellIndex].requirePositionChoose)
            {
                Cast(skills[spellIndex], player.gameObject.transform);
            }
            else
            {
                Cast(skills[spellIndex], this.transform);
            }
        }
    }

    public void Cast(Skill skill, Transform position)
    {
        GameObject skillObj = Instantiate(skill.skillVisual, position.position, position.rotation);
        SkillInstancedata data = skillObj.GetComponent<SkillInstancedata>();

        data.caster = this.gameObject;
        data.skill = skill;
    }
    private void CoolDown(int skillIndex)
    {
        StartCoroutine(SkillCoolDown(skillIndex));
    }
    IEnumerator SkillCoolDown(int skillIndex)
    {
        skillsCDStatus[skillIndex] = true;
        yield return new WaitForSeconds(skills[skillIndex].coolDown);
        skillsCDStatus[skillIndex] = false;
    }
}
