using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rally : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private Transform playerField;
    private Transform opponentField;

    private void OnEnable()
    {
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        playerField = GameObject.Find("PlayerPlayingField").transform;
        opponentField = GameObject.Find("OpponentPlayingField").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if it is the player's turn or the opponent's turn
        if (turnController.IsPlayerTurn)
        {
            //Iterate through each card in the player's field
            foreach (Card child in playerField)
            {
                //If it is not a Utility card, increase its attack by 1
                if (child.type != "Utility")
                {
                    child.attack += 1;
                }
            }
        }

        else
        {
            //Iterate through each card in the opponent's field
            foreach (Card child in opponentField)
            {
                //If it is not a Utility card, increase its attack by 1
                if (child.type != "Utility")
                {
                    child.attack += 1;
                }
            }
        }

        //Disable Rally
        this.GetComponent<Rally>().enabled = false;
    }
}
