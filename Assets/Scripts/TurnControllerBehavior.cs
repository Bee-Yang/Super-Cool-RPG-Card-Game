using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnControllerBehavior : MonoBehaviour
{
    private bool isPlayerTurn;
    private DeckBehavior player, enemy;

    public GameObject playerDeck, enemyDeck;

    public bool IsPlayerTurn {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }

    void Awake()
    {
        player = playerDeck.GetComponent<DeckBehavior>();
        enemy = enemyDeck.GetComponent<DeckBehavior>();
    }

    void Start()
    {
        isPlayerTurn = true;
        playerGo();
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
        /*
        if (playerDeck.transform.childCount > 0)
        {
            player.Draw();
        }
        */
    }
    void opponentGo()
    {
        //phases will be controlled here, turn ended automatically
        /*
        if (enemyDeck.transform.childCount > 0)
        {
            enemy.Draw();
        }
        */
        AlternateTurn();
    }

}