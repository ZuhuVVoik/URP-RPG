using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootUI : InventoryUI
{
    public GameObject nodeParent;
    public Text counter;

    public Lootable obj;
    public List<ItemInstance> loot = new List<ItemInstance>();
    private List<List<ItemInstance>> pages = new List<List<ItemInstance>>();

    public int itemsOnPage = 4;
    private int currentPageIndex = 0;
    private int pagesCount;

    public LootNode[] nodes;
    public void OnLootWindowOpen(List<ItemInstance> loot, Lootable obj)
    {
        this.loot = loot;
        this.obj = obj;

        currentPageIndex = 0;

        if(loot.Count != 0)
        {
            Paginate();
            OpenPage(currentPageIndex);
        }
        else
        {
            OpenEmptyPage();
        }
    }
    public void Paginate()
    {
        List<ItemInstance> page = new List<ItemInstance>();


        for(int i = 0; i < loot.Count; i++)
        {
            page.Add(loot[i]);

            if(page.Count == itemsOnPage || i == loot.Count - 1)
            {
                pages.Add(page);
                page = new List<ItemInstance>();
            }
        }
    }
    public void OpenPage(int pageIndex)
    {
        counter.text = (pageIndex + 1).ToString() + "/" + (pagesCount + 1).ToString();

        foreach (LootNode node in nodes)
        {
            node.gameObject.SetActive(true);
            node.ClearNode();
        }


        for (int i = 0; i < pages[pageIndex].Count; i++)
        {
            nodes[i].RefreshNode(pages[pageIndex][i]);
        }

        foreach (LootNode node in nodes)
        {
            if (node.itemInstance == null)
                node.gameObject.SetActive(false);
        }
    }

    public void OpenEmptyPage()
    {
        counter.text = "";

        foreach (LootNode node in nodes)
        {
            node.gameObject.SetActive(true);
            node.ClearNode();
        }

        foreach (LootNode node in nodes)
        {
            if (node.itemInstance == null)
                node.gameObject.SetActive(false);
        }
    }

    public void OpenNextPage()
    {
        currentPageIndex++;
        if (currentPageIndex > pagesCount - 1)
            currentPageIndex = 0;
        OpenPage(currentPageIndex);
    }
    public void OpenPrevPage()
    {
        currentPageIndex--;
        if (currentPageIndex < 0)
            currentPageIndex = pagesCount;
        OpenPage(currentPageIndex);
    }

    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }



    public void Loot(LootNode node)
    {

        InventoryManager.instance.AddItem(node.itemInstance);


        pages[currentPageIndex].Remove(node.itemInstance);
        loot.Remove(node.itemInstance);
        obj.cache.Remove(node.itemInstance);

        if (pages[currentPageIndex].Count == 0)
        {
            pages.Remove(pages[currentPageIndex]);
            pagesCount = pages.Count;

            if(currentPageIndex == pagesCount && pagesCount > 0)
            {
                currentPageIndex--;
            }
        }

        Paginate();
        if(loot.Count > 0)
        {
            OpenPage(currentPageIndex);
        }
        else
        {
            OpenEmptyPage();
        }
    }
    public void LootAll()
    {
        foreach(ItemInstance item in loot)
        {
            InventoryManager.instance.AddItem(item);
        }

        loot.Clear();
        obj.cache.Clear();

        foreach (LootNode node in nodes)
        {
            node.ClearNode();
        }

        CloseWindow();
    }
}
