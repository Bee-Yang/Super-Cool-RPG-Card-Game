﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButtonBehavior : MonoBehaviour
{
    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
