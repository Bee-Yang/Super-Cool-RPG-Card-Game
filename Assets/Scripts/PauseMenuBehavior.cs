using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenu;
    private TurnControllerBehavior turnController;

    void Start () {
        pauseMenu.SetActive(false);
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Pause() {
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void ForfeitGame(){
        Debug.Log("Forfeiting game...");
        turnController.SetPhase(-2);
    }

    public void QuitGame(){
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
