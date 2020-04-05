using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadialPickupUI : MonoBehaviour
{
    public Image radialFill;
    public GameObject playerRef;
    public Camera cam;
    public AudioClip confirm;
    bool isFilling;

    [HideInInspector]
    public ObjectType objectType;

    private AudioSource ac;

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ac = GetComponent<AudioSource>();
    }
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

    public void SetFilling(bool state, ObjectType type)
    {
        objectType = type;
        isFilling = state;
    }
    void CheckIfFull()
    {
        if(radialFill.fillAmount == 1)
        {
            ac.PlayOneShot(confirm);
            isFilling = false;
            radialFill.fillAmount = 0;
            if (objectType == ObjectType.Resource)
                playerRef.GetComponent<PlayerPickup>().RadialUIFull();
            else
                playerRef.GetComponent<PlayerPickup>().activeLeverControl.Interact();
        }
    }

    void PositionObject()
    {
        transform.position = cam.WorldToScreenPoint(AbovePlayer());
    }
}
