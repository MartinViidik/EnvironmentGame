using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public GameObject pickupUI;
    private GameObject resourceRef;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Resource"))
        {
            resourceRef = col.gameObject;
            SetPickupUI(true);        
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Resource"))
        {
            resourceRef = null;
            SetPickupUI(false);
        }
    }

    public void PickupItem()
    {
        resourceRef.SetActive(false);
        resourceRef = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && resourceRef != null)
        {
            SetRadialFill(true);
            Debug.Log("test");
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
        pickupUI.GetComponent<RadialPickupUI>().SetFilling(state);
    }
}
