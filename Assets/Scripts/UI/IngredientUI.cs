using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    public ResourceType resourceType;
    [SerializeField]
    Image spriteRenderer;
    [SerializeField]
    Text ingredientCountText;
    int currentIngredientCount = 0;

    private void Start()
    {
        ingredientCountText.text = currentIngredientCount.ToString();
        spriteRenderer.color = Color.black;
    }

    public void UpdateIngredientCount(int count)
    {
        spriteRenderer.color = Color.white;
        currentIngredientCount += count;
        ingredientCountText.text = currentIngredientCount.ToString();
    }
}
