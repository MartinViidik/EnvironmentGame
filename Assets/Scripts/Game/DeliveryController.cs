using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    private Recepie recepie;

    public List<Recepie> possible_recepies = new List<Recepie>();
    public GameObject _playerRef;
    public DeliveryScreen screen;
    private void Awake()
    {
        GenerateTask();
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
        screen.EnableDeliveryScreen();
    }

    public void GenerateTask()
    {
        recepie = GetRandomRecepie();
        screen.GetComponent<DeliveryScreen>().NewTask(TaskDescription());
    }
    public string TaskDescription()
    {
        return "I have to get: " + recepie.name.ToString() + " for that, get me: " + recepie.amount + "" + recepie.resource;
    }
    public Recepie GetRandomRecepie()
    {
        Recepie recepie = possible_recepies[Random.Range(0, possible_recepies.Count)];
        possible_recepies.Remove(recepie);
        return recepie;
    }
}
