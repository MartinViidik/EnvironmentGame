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
        if (ingredientCountText == null)
            ingredientCountText = transform.GetChild(0).gameObject.GetComponent<Text>();
        if (spriteRenderer == null)
            spriteRenderer = transform.GetChild(1).gameObject.GetComponent<Image>();

        ingredientCountText.text = currentIngredientCount.ToString();
        if (ingredientCountText == null)
            Debug.Log(gameObject.name);
        spriteRenderer.color = Color.black;
    }

    public void UpdateIngredientCount(int count)
    {
        if (ingredientCountText == null)
            ingredientCountText = transform.GetChild(0).gameObject.GetComponent<Text>();
        if (spriteRenderer == null)
            spriteRenderer = transform.GetChild(1).gameObject.GetComponent<Image>();
        spriteRenderer.color = Color.white;
        currentIngredientCount += count;
        ingredientCountText.text = currentIngredientCount.ToString();
    }
}
