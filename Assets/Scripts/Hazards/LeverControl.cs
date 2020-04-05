using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControl : MonoBehaviour
{

    bool canInteract = false;
    bool areLinkedWiresActive;

    [SerializeField]
    private GameObject pickupUI;

    [SerializeField]
    Wire[] linkedWires;
    [SerializeField]
    Animator leverAnimator;

    private PlayerPickup playerPickup;

    float delayBeforeShowingInteractAgain = 2f;
    float nextTimeWhenShowInteract;
    bool waitingForPickupToBeActive = false;

    private AudioSource ac;
    public AudioClip[] turnOn;
    public AudioClip[] turnOff;

    private void Awake()
    {
        playerPickup = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
        ac = GetComponent<AudioSource>();
    }

    void PlaySound(AudioClip[] clip)
    {
        if (ac.isPlaying)
        {
            ac.Stop();
        }
        ac.PlayOneShot(clip[Random.Range(0, clip.Length)]);
    }

    public void Interact()
    {
        if (areLinkedWiresActive)
        {
            PlaySound(turnOff);
        } else {
            PlaySound(turnOn);
        }
       areLinkedWiresActive = linkedWires[0].canHurtPlayer;
        Debug.Log("linked wires was " + areLinkedWiresActive + " " + Time.time);
        areLinkedWiresActive = !areLinkedWiresActive;
       Debug.Log("linked wires is " + areLinkedWiresActive + " " + Time.time);

        foreach (Wire wire in linkedWires)
        {
            wire.Toggle(areLinkedWiresActive);
        }
        //Debug.Log("set linked wires to " + areLinkedWiresActive + " " + Time.time);
        leverAnimator.SetBool("down", !areLinkedWiresActive);
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
        
        if (!pickupUI.activeInHierarchy && !waitingForPickupToBeActive)
        {
            waitingForPickupToBeActive = true;
            nextTimeWhenShowInteract = Time.time + delayBeforeShowingInteractAgain;
            return;
        }

        if (Time.time < nextTimeWhenShowInteract)
            return;
        waitingForPickupToBeActive = false;
        pickupUI.SetActive(true);

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
