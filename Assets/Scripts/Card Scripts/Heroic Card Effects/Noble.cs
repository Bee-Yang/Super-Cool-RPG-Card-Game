using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noble : MonoBehaviour
{
    private TurnControllerBehavior controller;
    private Transform playerField;
    private Transform opponentField;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        playerField = GameObject.Find("PlayerPlayingField").transform;
        opponentField = GameObject.Find("OpponentPlayingField").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if it's the player's turn or not
        if (controller.IsPlayerTurn)
        {
            NobleEffect(playerField);
        }

        else
        {
            NobleEffect(opponentField);
        }

        //Disable the script
        this.GetComponent<Noble>().enabled = false;
    }
    void NobleEffect(Transform field)
    {
        //Check if there are other cards on the field
        if (field.childCount > 1)
        {
            Transform card;
            CardAttributes cardAtt;

            //Loop through all the cards in the player's field
            for (int i = 0; i < field.childCount; i++)
            {
                card = field.GetChild(i);
                cardAtt = card.GetComponent<CardAttributes>();

                //Check if the card is the heroic card; if not, buff it.
                if (cardAtt.GetID() != 10)
                {
                    cardAtt.SetAttack(cardAtt.GetAttack() + 1);
                    cardAtt.SetCurrentHealth(cardAtt.GetCurrentHealth() + 1);
                }

            }
        }
    }
}
