using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();
    public void AddToInventory(Resource resource, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject newResource = Instantiate(resource).gameObject;
            newResource.name = resource.type.ToString();
            newResource.hideFlags = HideFlags.HideInHierarchy;
            newResource.SetActive(false);
            inventory.Add(newResource);
        }
    }
}
