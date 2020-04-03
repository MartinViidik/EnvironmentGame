using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    private Recepie recepie;

    public Recepie[] possible_recepies;
    public GameObject _playerRef;
    public List<GameObject> forDeletion = new List<GameObject>();
    private void Awake()
    {
        recepie = possible_recepies[0];
        Debug.Log(recepie.name);
    }

    public void CheckInventory()
    {
        int amount = recepie.amount;

        int j = 0;
        PlayerInventory Inv = _playerRef.GetComponent<PlayerInventory>();
        for(int i = 0; i != Inv.inventory.Count; i++)
        {
            if(recepie.resource == Inv.inventory[i].GetComponent<Resource>().type)
            {
                j++;
                forDeletion.Add(Inv.inventory[i].gameObject);
                if (j == amount)
                {
                    Debug.Log("Done");
                    _playerRef.GetComponent<PlayerInventory>().RemoveFromInventory(recepie.gameObject, amount);
                    break;
                }
            }
        }
        forDeletion.Clear();
    }
}
