using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1.0)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            } else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
