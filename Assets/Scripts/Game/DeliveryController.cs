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
        for (int i = 0; i < recepie.amount.Length; i++)
        {
            if (CheckOneIngredient(i) == false)
                return false;
        }
        return true;
    }

    private bool CheckOneIngredient(int ingredientID)
    {
        int amount = recepie.amount[ingredientID];
        int j = 0;
        PlayerInventory Inv = _playerRef.GetComponent<PlayerInventory>();
        for (int i = 0; i != Inv.inventory.Count; i++)
        {
            if (recepie.resource[ingredientID] == Inv.inventory[i].GetComponent<Resource>().type)
            {
                if (j == amount)
                {
                    break;
                }
                else
                {
                    j++;
                }
            }
        }
        Debug.Log("you have " + j + " from " + amount + " of " + recepie.resource[ingredientID].ToString());
        if (j >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveFrom()
    {
        _playerRef.GetComponent<PlayerInventory>().RemoveFromInventory(recepie);
        Debug.Log(possible_recepies.Count);
        if(possible_recepies.Count != 0 )
        {
            screen.EnableDeliveryScreen();
            GenerateTask();
        } else {
            screen.EnableDeliveryScreen();
            screen.gameEnded = true;
        }
    }

    public void GenerateTask()
    {
        recepie = GetRandomRecepie();
        screen.GetComponent<DeliveryScreen>().NewTask(TaskDescription());
    }
    public string TaskDescription()
    {
        string description = "I have to get: " + recepie.name.ToString() + " for that, get me: ";
        for (int i = 0; i < recepie.amount.Length; i++)
        {
            if (i > 0)
                description += "\n AND \n";
            description += recepie.amount[i] + " " + recepie.resource[i];

        }
        return description;
    }
    public Recepie GetRandomRecepie()
    {
        Recepie recepie = possible_recepies[Random.Range(0, possible_recepies.Count)];
        possible_recepies.Remove(recepie);
        return recepie;
    }
}
