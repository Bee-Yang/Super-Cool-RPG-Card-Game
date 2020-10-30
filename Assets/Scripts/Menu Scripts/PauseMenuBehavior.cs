using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenu;
    private TurnControllerBehavior turnController;

    void Start()
    {
        pauseMenu.GetComponent<Canvas>().enabled = false;
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.GetComponent<Canvas>().enabled = false;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenu.GetComponent<Canvas>().enabled = true;
        isPaused = true;
    }

    public void ForfeitGame()
    {
        Debug.Log("Forfeiting game...");
        pauseMenu.GetComponent<Canvas>().enabled = false;
        turnController.SetPhase(-2);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
