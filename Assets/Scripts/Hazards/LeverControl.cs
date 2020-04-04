using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControl : MonoBehaviour
{

    bool canInteract = false;
    bool areLinkedWiresActive = true;

    [SerializeField]
    private GameObject pickupUI;

    [SerializeField]
    Wire[] linkedWires;

    private PlayerPickup playerPickup;

    private void Awake()
    {
        playerPickup = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
    }

    public void Interact()
    {
        areLinkedWiresActive = !areLinkedWiresActive;

        foreach(Wire wire in linkedWires)
        {
            wire.canHurtPlayer = areLinkedWiresActive;
        }
        SetPickupUI(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInteract = true;
            SetPickupUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInteract = false;
            SetPickupUI(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Player")
                canInteract = true;
    }

    private void Update()
    {
        if (!canInteract)
            return;
        Debug.Log("CAN INTERACT");
        if (Input.GetKeyDown(KeyCode.E) && pickupUI.activeInHierarchy)
        {
            SetRadialFill(true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            SetRadialFill(false);
        }
    }

    public void SetRadialFill(bool state)
    {
        playerPickup.activeLeverControl = this;
        pickupUI.GetComponent<RadialPickupUI>().SetFilling(state, ObjectType.Lever);
    }

    void SetPickupUI(bool state)
    {
        pickupUI.SetActive(state);
    }

}
