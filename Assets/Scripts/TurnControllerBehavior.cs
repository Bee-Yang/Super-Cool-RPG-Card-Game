﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnControllerBehavior : MonoBehaviour
{
    private bool isPlayerTurn;

    public int phase;

    public bool IsPlayerTurn {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }

    void Start()
    {
        isPlayerTurn = true;
        playerGo();
        phase = 0;

        DisableAllPhases();
    }

    void Update()
    {
        if (isPlayerTurn) {
            playerGo();
        } 
        else {
            opponentGo();
        }
    }
    public void AlternateTurn() {
        isPlayerTurn = !isPlayerTurn;
    }

    void playerGo()
    {
        //add calls for functions for draw card, and begin battle here. turn ended manually by button
        switch(phase)
        {
            // Enable the script for drawing 5 cards at the beginning of the duel
            case 0:
                this.GetComponent<Beginning>().enabled = true;
                break;

            // Enable the script for drawing phase
            case 1:
                this.GetComponent<DrawPhase>().enabled = true;
                break;

            default:
                DisableAllPhases();
                break;
        }
    }
    void opponentGo()
    {
        //phases will be controlled here, turn ended automatically
        AlternateTurn();
    }

    public bool GetIsPlayerTurn()
    {
        return this.isPlayerTurn;
    }

    public int GetPhase()
    {
        return this.phase;
    }

    public void SetPhase(int nextPhase)
    {
        this.phase = nextPhase;
    }

    public void DisableAllPhases()
    {
        this.GetComponent<Beginning>().enabled = false;
        this.GetComponent<DrawPhase>().enabled = false;
    }
}