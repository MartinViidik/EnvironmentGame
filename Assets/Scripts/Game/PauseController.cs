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
                AudioListener.volume = 0;
                Time.timeScale = 0;
            } else
            {
                pauseMenu.SetActive(false);
                AudioListener.volume = 1;
                Time.timeScale = 1;
            }
        }
    }
}
