using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    [Header("Data fields")]
    public List<CraftRecipe> avaibleRecipes;
    public List<CraftRecipe> startRecipes;

    public delegate void OnRecipeChanged();
    public OnRecipeChanged onRecipeChangedCallback;

    public bool AddRecipe(CraftRecipe craftRecipe)
    {
        if (!avaibleRecipes.Contains(craftRecipe))
        {
            avaibleRecipes.Add(craftRecipe);

            if (onRecipeChangedCallback != null)
            {
                onRecipeChangedCallback.Invoke();
            }

            return true;
        }
        return false;
    }
}
