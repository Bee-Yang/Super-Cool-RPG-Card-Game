﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private TurnControllerBehavior turnController;
    private double time;
    private bool start;

    public GameObject playerTurn, enemyTurn;

    void OnEnable()
    {
        time = 0;
        start = true;
    }
 
    // Start is called before the first frame update
    void Start()
    {
        turnController = this.GetComponent<TurnControllerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnNotification();

        // Temporary code to directly enter battle phase on opponents turn after draw phase
        if(!turnController.IsPlayerTurn && !this.start)
        {
            turnController.DisableAllPhases();
            turnController.SetPhase(3);
        }
    }

    private void SetStart(bool status)
    {
        this.start = status;
    }

    public void TurnNotification()
    {
        if (this.start)
        {
            // Add time delay to the timer
            this.time += Time.deltaTime;

            if (this.time > timeDelay)
            {
                // Notify the user about whose turn it is by enabling/disabling the turn notification after a time delay
                if (turnController.IsPlayerTurn)
                {
                    playerTurn.SetActive(!playerTurn.activeSelf);
                }
                else
                {
                    enemyTurn.SetActive(!enemyTurn.activeSelf);
                }

                // Disable this method once user has been notified about whose turn it is
                if (!playerTurn.activeSelf && !enemyTurn.activeSelf)
                {
                    this.start = false;
                }

                // Reset the timer
                this.time = 0;
            }
        }
    }
}
