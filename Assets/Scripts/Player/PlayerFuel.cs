using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour
{
    private bool outOfFuel = false;
    private float maxFuel = 100;
    private float currentFuel;

    [SerializeField]
    Image fuelBar;

    private void Start()
    {
        currentFuel = maxFuel;
        UpdateFuelBar();
    }

    public void LoseFuel(float amount)
    {
        if (outOfFuel)
            return;

        currentFuel -= amount;
        UpdateFuelBar();
        if(currentFuel <= 0 && !outOfFuel)
        {
            outOfFuel = true;
            amount = 0;
            Lose();
        }
    }

    private void UpdateFuelBar()
    {
        fuelBar.fillAmount = (currentFuel * 1f) / maxFuel;
    }

    private void Lose()
    {
        Debug.Log("lose");
    }
   
}
