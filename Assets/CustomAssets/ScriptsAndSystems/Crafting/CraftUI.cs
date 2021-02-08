using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftUI : MonoBehaviour
{
    [Header("References")]
    public GameObject CraftView;
    public GameObject RecipeTemplate;
    public RecipesManager recipesManager;

    [Header("Public Variables")]
    public List<CraftRecipeUI> currentNodes;
    public List<CraftRecipe> currentRecipes;

    public float nodePadding;
    private float firstPos;
    private float nodeHeight;
    private float currentNodePosY;


    private void Start()
    {
        recipesManager.onRecipeChangedCallback += UpdateUI;

        nodeHeight = RecipeTemplate.GetComponent<RectTransform>().sizeDelta.y;
        currentNodePosY = nodeHeight + nodePadding;

        List<CraftRecipe> recipes = recipesManager.avaibleRecipes;

        for(int i = 0; i < recipes.Count; i++)
        {
            currentRecipes.Add(recipes[i]);
            GenerateNode(recipes[i]);

            if (i == 0)
            {
                firstPos = currentNodes[i].transform.position.y - nodeHeight;
                currentNodes[i].gameObject.transform.position = new Vector3(currentNodes[i].gameObject.transform.position.x, firstPos, currentNodes[i].gameObject.transform.position.z);

            }
        }


    }

    public void UpdateUI()
    {
        List<CraftRecipe> recipes = recipesManager.avaibleRecipes;

        foreach(CraftRecipe recipe in recipes)
        {
            if (!currentRecipes.Contains(recipe))
            {
                currentRecipes.Add(recipe);
                GenerateNode(recipe);
            }
        }
    }
    public void RecalculatePosition()
    {
        float pos = firstPos;

        foreach(CraftRecipeUI recipeUI in currentNodes)
        {
            pos -= nodeHeight*1.5f - nodePadding;
        }

        currentNodePosY = pos;
    }
    public void GenerateNode(CraftRecipe recipe)
    {
        GameObject obj = Instantiate(RecipeTemplate, CraftView.transform);
        CraftRecipeUI recipeUI = obj.GetComponent<CraftRecipeUI>();
        
        recipeUI.UpdateData(recipe);
        currentNodes.Add(recipeUI);
        
        recipeUI.gameObject.transform.position = new Vector3(recipeUI.gameObject.transform.position.x, recipeUI.gameObject.transform.position.y + currentNodePosY, recipeUI.gameObject.transform.position.z);

        RecalculatePosition();
    }
}
