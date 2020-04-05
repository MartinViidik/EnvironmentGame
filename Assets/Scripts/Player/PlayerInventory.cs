using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject DeliveryController;

    public IngredientUI[] ingredientUIs;

    public List<GameObject> inventory = new List<GameObject>();
    public void AddToInventory(Resource resource, int amount)
    {
        for(int i = 0; i != amount; i++)
        {
            GameObject newResource = Instantiate(resource).gameObject;
            newResource.name = resource.type.ToString();
            newResource.hideFlags = HideFlags.HideInHierarchy;
            newResource.SetActive(false);
            inventory.Add(newResource);
        }

        foreach (IngredientUI ingredient in ingredientUIs)
        {
            if (ingredient.resourceType == resource.type)
                ingredient.UpdateIngredientCount(amount);
        }
        DeliveryController.GetComponent<DeliveryController>().CheckInventory();
    }

    public void RemoveFromInventory(Recepie recipe)//, GameObject resource, int amount)
    {
        for (int i = 0; i < recipe.resource.Length; i++)
        {
            RemoveIngredientFromInventory(i, recipe.gameObject, recipe.amount[i]);
        }
    }

    private void RemoveIngredientFromInventory(int ingredientID, GameObject resource, int amount)
    {
        int j = 0;
        for (int i = 0; i != inventory.Count; i++)
        {
            if (inventory[i].GetComponent<Resource>().type == resource.GetComponent<Recepie>().resource[ingredientID])
            {
                foreach (IngredientUI ingredient in ingredientUIs)
                {
                    if (ingredient.resourceType == inventory[i].GetComponent<Resource>().type)
                    {
                        Debug.Log("removing " + amount + " of " + inventory[i].GetComponent<Resource>().type);
                        ingredient.UpdateIngredientCount(-1);
                    }
                }
                inventory.Remove(inventory[i]);
                j++;
                if (j == amount)
                {
                    break;
                }
            }
        }

    }

    public bool InventoryValid()
    {
       return DeliveryController.GetComponent<DeliveryController>().CheckInventory();
    }
}
