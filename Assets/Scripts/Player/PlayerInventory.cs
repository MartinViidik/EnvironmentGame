using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject DeliveryController;

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
        DeliveryController.GetComponent<DeliveryController>().CheckInventory();
    }

    public void RemoveFromInventory(GameObject resource, int amount)
    {
        int j = 0;
        for(int i = 0; i != inventory.Count; i++)
        {
            if(inventory[i].GetComponent<Resource>().type == resource.GetComponent<Recepie>().resource)
            {
                inventory.Remove(inventory[i]);
                j++;
                if (j == amount)
                {
                    break;
                }
            }
        }
    }
}
