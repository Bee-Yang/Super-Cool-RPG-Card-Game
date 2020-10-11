using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnControllerBehavior : MonoBehaviour
{
    private bool isPlayerTurn;
    public const int beginning = 0, drawPhase = 1, playPhase = 2, battlePhase = 3;

    public int phase;

    public bool IsPlayerTurn {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }

    void Start()
    {
        isPlayerTurn = true;
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
            case beginning:
                this.GetComponent<Beginning>().enabled = true;
                break;

            // Enable the script for drawing phase
            case drawPhase:
                this.GetComponent<DrawPhase>().enabled = true;
                break;
               
            // Enable the script for playing phase
            case playPhase:
                this.GetComponent<PlayingPhase>().enabled = true;
                break;
            
            // Enable the script for battle phase
            case battlePhase:
                this.GetComponent<BattlePhase>().enabled = true;
                break;

            default:
                DisableAllPhases();
                break;
        }
    }
    void opponentGo()
    {
        //phases will be controlled here, turn ended automatically
        switch (phase)
        {
            // Enable the script for drawing 5 cards at the beginning of the duel
            case beginning:
                this.GetComponent<Beginning>().enabled = true;
                break;

            // Enable the script for drawing phase
            case drawPhase:
                this.GetComponent<DrawPhase>().enabled = true;
                break;

            // Enable the script for playing phase
            case playPhase:
                this.GetComponent<PlayingPhase>().enabled = true;
                break;

            // Enable the script for battle phase
            case battlePhase:
                this.GetComponent<BattlePhase>().enabled = true;
                break;

            default:
                DisableAllPhases();
                break;
        }
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
        this.GetComponent<PlayingPhase>().enabled = false;
        this.GetComponent<BattlePhase>().enabled = false;
    }

    // Enable dragging for player
    public void EnableDraggingForPlayer()
    {
        Transform hand = GameObject.Find("Hand-Player").transform;

        for (int i = 0; i < hand.childCount; ++i)
        {
            hand.GetChild(i).GetComponent<CardBehavior>().SetDraggable(true);
        }
    }

    // Disable dragging for player
    public void DisableDraggingForPlayer()
    {
        Transform hand = GameObject.Find("Hand-Player").transform;

        for (int i = 0; i < hand.childCount; ++i)
        {
            hand.GetChild(i).GetComponent<CardBehavior>().SetDraggable(false);
        }
    }
}