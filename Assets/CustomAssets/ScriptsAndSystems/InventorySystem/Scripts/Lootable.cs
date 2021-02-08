using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootGenType { Once, ByTime, EveryTimeContainerOpens}
public class Lootable : InteractableObject
{
    public LootGenType generationType;
    public float lootTime;
    [Tooltip("Только для генерации по времени")]
    public float timeToGenerateLoot = 1f;

    private LootTable lootTable;
    public List<ItemInstance> cache = new List<ItemInstance>();
    private bool openedOnce = false;
    private bool generateAgain = false;


    protected PlayerMotor player;
    private void Start()
    {
        lootTable = GetComponent<LootTable>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && player.freezed && player.interracting)
        {
            StopCoroutine(LootRoutine());
            player.interracting = false;
            player.freezed = false;
            progressBar.StopProgress();
        }
    }

    public override void Interract()
    {
        player = InventoryManager.instance.player;
        /* Включить анимации и пустить корутину сбора */

        StartCoroutine(LootRoutine());
    }
    public void Use()
    {
        bindedUI.gameObject.SetActive(true);
        LootUI uI = bindedUI as LootUI;


        if (!openedOnce && generationType == LootGenType.Once)
        {
            cache = lootTable.GetLoot();
            openedOnce = true;
        }
        if(generationType == LootGenType.ByTime)
        {
            if (openedOnce)
            {
                cache = lootTable.GetLoot();
                openedOnce = true;
            }
            else
            {
                if (generateAgain)
                {
                    cache = lootTable.GetLoot();
                    StartCoroutine(LootByTimeRoutine());
                }
            }
        }

        uI.gameObject.SetActive(true);
        uI.OnLootWindowOpen(cache, this);
    }


    IEnumerator LootRoutine()
    {
        /* Эта корутина на время как раз и замораживает */

        progressBar.StartProgressBar(lootTime);
        player.freezed = true;
        player.interracting = true;
        yield return new WaitForSeconds(lootTime);
        player.freezed = false;
        player.interracting = true;
        this.Use();
    }
    IEnumerator LootByTimeRoutine()
    {
        generateAgain = false;
        yield return new WaitForSeconds(timeToGenerateLoot);
        generateAgain = true;
    }
}
