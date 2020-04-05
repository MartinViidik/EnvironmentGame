using UnityEngine;
using TMPro;

public class DeliveryScreen : MonoBehaviour
{
    public MenuController menu;
    public PlayerMovement playerRef;
    public TMP_Text TaskText;
    public string task;
    public bool gameEnded = false;

    private void Awake()
    {
        if (playerRef == null)
            playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void NewTask(string newTask)
    {
        task = newTask;
        TaskText.text = newTask;
    }

    private void Update()
    {
        if(isActiveAndEnabled && Input.GetKeyDown(KeyCode.E)){
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        playerRef.active = false;
    }

    private void OnDisable()
    {
        playerRef.active = true;
        if (gameEnded)
        {
            menu.ForceReturnMenu();
        }
    }

    public void EnableDeliveryScreen()
    {
        gameObject.SetActive(true);
        TaskText.text = "thanks bro";
    }

}
