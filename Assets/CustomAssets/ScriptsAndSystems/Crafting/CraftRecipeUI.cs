using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipeUI : MonoBehaviour
{
    public CraftRecipe recipe;

    public List<CraftImage> materials;
    public List<CraftImage> results;

    public void UpdateData(CraftRecipe recipe)
    {
        this.recipe = recipe;

        for(int i = 0; i < materials.Count; i++)
        {
            if(i < recipe.Materials.Count)
            {
                if (recipe.Materials[i].Item != null)
                {
                    materials[i].UpdateItem(recipe.Materials[i]);
                }
            }
            else
            {
                materials[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < results.Count; i++)
        {
            if (i < recipe.Results.Count)
            {
                if (recipe.Results[i].Item != null)
                {
                    results[i].UpdateItem(recipe.Results[i]);
                }
            }
            else
            {
                results[i].gameObject.SetActive(false);
            }
        }
    }

    public void Craft()
    {
        this.recipe.Craft();
    }
}
