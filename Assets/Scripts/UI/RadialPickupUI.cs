using UnityEngine;
using UnityEngine.UI;

public class RadialPickupUI : MonoBehaviour
{
    public Image radialFill;
    public GameObject playerRef;
    public Camera cam;
    bool isFilling;

    private void OnEnable()
    {
        InvokeRepeating("PositionObject", 0.0f, 0.01f);
    }

    void Update()
    {
        if (isFilling)
        {
            radialFill.fillAmount += Time.deltaTime;
            CheckIfFull();
        } else {
            radialFill.fillAmount -= Time.deltaTime;
        }
    }

    Vector2 AbovePlayer()
    {
        float x = playerRef.transform.position.x;
        float y = playerRef.transform.position.y + 2;
        return new Vector2(x, y);
    }

    public void SetFilling(bool state)
    {
        isFilling = state;
    }
    void CheckIfFull()
    {
        if(radialFill.fillAmount == 1)
        {
            isFilling = false;
            radialFill.fillAmount = 0;
            playerRef.GetComponent<PlayerPickup>().RadialUIFull();
        }
    }

    void PositionObject()
    {
        transform.position = cam.WorldToScreenPoint(AbovePlayer());
    }
}
