﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationBehavior : MonoBehaviour
{
    private TMP_Text currText;
    private TurnControllerBehavior turnController;
    const int defend = 1, attack = 2;

    // Start is called before the first frame update
    void Start()
    {
        currText = GetComponent<TMP_Text>();
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        currText.text = "Good luck, player. May Todd smile upon thee.";
    }

    //Call to show who has played which card
    public void PlayCard(CardAttributes card)
    {
        string prefix = "";

        if (turnController.IsPlayerTurn)
        {
            prefix = "You have played ";
        }
        else
        {
            prefix = "Your opponent has played ";
        }

        currText.text = prefix + card.GetName();
    }

    public void HeroicCard(CardAttributes card)
    {
        string prefix = "";

        if (turnController.IsPlayerTurn)
        {
            prefix = "You have played ";
        }
        else
        {
            prefix = "Your opponent has played ";
        }

        currText.text = prefix + "Heroic Card " + card.GetName() + "!";
    }

    //Call to show who has attacked which card
    public void Attack(CardAttributes attacker, CardAttributes defender)
    {
        string prefix = "";

        if (turnController.IsPlayerTurn)
        {
            prefix = "You have attacked ";
        }
        else
        {
            prefix = "Your opponent has attacked ";
        }

        currText.text = prefix + defender.GetName() + " with " + attacker.GetName();
    }

    public void Destroy(CardAttributes card)
    {
        currText.text = card.GetName() + " has been sent to the afterlife";
    }

    //Call to have the Notification Text display an instruction to the player
    public void Instruct(int instruction)
    {
        //This will be called with an input to designate what instruction to give the player
        switch (instruction)
        {
            case defend:
                currText.text = "Pick which cards will defend";
                break;

            case attack:
                currText.text = "Pick which cards will attack";
                break;

            default:
                break;
        }
    }
}
