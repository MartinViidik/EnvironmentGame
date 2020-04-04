using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    private Recepie recepie;

    public Recepie[] possible_recepies;
    public GameObject _playerRef;
    private void Awake()
    {
        recepie = possible_recepies[0];
    }

    public bool CheckInventory()
    {
        int amount = recepie.amount;
        int j = 0;
        PlayerInventory Inv = _playerRef.GetComponent<PlayerInventory>();
        for(int i = 0; i != Inv.inventory.Count; i++)
        {
            if(recepie.resource == Inv.inventory[i].GetComponent<Resource>().type)
            {
                if (j == amount)
                {
                    break;
                } else {
                    j++;
                }
            }
        }
        if (j >= amount)
        {
            return true;
        } else {
            return false;
        }
    }

    public void RemoveFrom()
    {
        _playerRef.GetComponent<PlayerInventory>().RemoveFromInventory(recepie.gameObject, recepie.amount);
    }
}
