using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : MonoBehaviour
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
            EvilEffect(opponentField);
        }

        else
        {
            EvilEffect(playerField);
        }

        //Disable the script
        this.GetComponent<Evil>().enabled = false;
    }
    void EvilEffect(Transform targetField)
    {
        //Check if the opponent has any cards on the field
        if (targetField.childCount > 0)
        {
            //Generate a random number between 0 and the highest index inclusively
            System.Random rnd = new System.Random();
            int rndNum = rnd.Next(0, targetField.childCount);

            //Pull the random card
            Transform card = targetField.GetChild(rndNum);

            //Set the card's health to 0, and destroy it
            card.GetComponent<CardAttributes>().SetCurrentHealth(0);
            card.GetComponent<CardBehavior>().SetDestroyed();
        }
    }
}
