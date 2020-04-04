using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;
    public GameObject mainMenu;

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
        Application.Quit();
    }
}
