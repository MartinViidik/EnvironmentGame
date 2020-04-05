using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject credits;
    public GameObject mainMenu;

    public GameObject areYouSure;
    
    public enum quitState
    {
        ReturnMenu,
        QuitGame
    }

    public quitState quit = quitState.ReturnMenu;

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        quit = quitState.QuitGame;
        areYouSure.SetActive(true);
    }

    public void ReturnToMenu()
    {
        quit = quitState.ReturnMenu;
        areYouSure.SetActive(true);
    }

    public void ForceReturnMenu()
    {
        AudioListener.volume = 1;
        SceneManager.LoadScene(0);
    }

    public void EnableAreYouSure()
    {
        areYouSure.SetActive(true);
    }

    public void ImNotSure()
    {
        areYouSure.SetActive(false);
    }

    public void ImSure()
    {
        if(quit == quitState.QuitGame)
        {
            Application.Quit();
        } else {
            Time.timeScale = 1;
            AudioListener.volume = 1;
            SceneManager.LoadScene(0);
        }
    }
}
