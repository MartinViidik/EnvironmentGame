using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public GameObject pickupUI;
    private GameObject resourceRef;
    private GameObject deliveryPoint;
    PlayerInventory inventory;

    [HideInInspector]
    public LeverControl activeLeverControl;
    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Resource"))
        {
            resourceRef = col.gameObject;
            SetPickupUI(true);        
        }
        if (col.gameObject.CompareTag("DeliveryPoint"))
        {
            if (inventory.InventoryValid())
            {
                deliveryPoint = col.gameObject;
                SetPickupUI(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Resource"))
        {
            resourceRef = null;
            SetPickupUI(false);
        }
        if (col.gameObject.CompareTag("DeliveryPoint"))
        {
            deliveryPoint = null;
            SetPickupUI(false);
        }
    }

    public void RadialUIFull()
    {
        if (resourceRef)
        {
            PickupItem();
        }
        if (deliveryPoint)
        {
            DeliverItem();
        }
    }

    public void PickupItem()
    {
        SetPickupUI(false);
        FetchItemValues();
        resourceRef.GetComponent<Dissolve>().SetDissolving(true);
        resourceRef = null;
    }

    public void DeliverItem()
    {
        deliveryPoint.GetComponent<DeliveryController>().RemoveFrom();
        SetPickupUI(false);
        deliveryPoint = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupUI.activeInHierarchy)
        {
            SetRadialFill(true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            SetRadialFill(false);
        }
    }

    void SetPickupUI(bool state)
    {
        pickupUI.SetActive(state);
    }

    public void SetRadialFill(bool state)
    {
        pickupUI.GetComponent<RadialPickupUI>().SetFilling(state, ObjectType.Resource);
    }

    public void FetchItemValues()
    {
        Resource resource = resourceRef.GetComponent<Resource>();
        GetComponent<PlayerInventory>().AddToInventory(resource, resource.amount);
    }
}
