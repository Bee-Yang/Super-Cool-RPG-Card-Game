using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationBehavior : MonoBehaviour
{
    private TMP_Text currText;

    // Start is called before the first frame update
    void Start()
    {
        currText = GetComponent<TMP_Text>();
        currText.text = "Good luck, player. May Todd smile upon thee.";
    }

    //Call to show who has played which card
    public void playCard(bool isPlayerTurn, Card card)
    {
        string prefix = "";

        if (isPlayerTurn)
        {
            prefix = "You have played ";
        }
        else
        {
            prefix = "Your opponent has played ";
        }

        currText.text = prefix + card.cardName;
    }

    public void heroicCard(bool isPlayerTurn, Card card)
    {
        string prefix = "";

        if (isPlayerTurn)
        {
            prefix = "You have played ";
        }
        else
        {
            prefix = "Your opponent has played ";
        }

        currText.text = prefix + "Heroic Card " + card.cardName + "!";
    }

    //Call to show who has attacked which card
    public void attack(bool isPlayerTurn, Card attacker, Card defender)
    {
        string prefix = "";

        if (isPlayerTurn)
        {
            prefix = "You have attacked ";
        }
        else
        {
            prefix = "Your opponent has attacked ";
        }

        currText.text = prefix + defender.cardName + " with " + attacker.cardName;
    }

    public void destroy(Card card)
    {
        currText.text = card.cardName + " has been sent to the afterlife";
    }

    //Call to have the Notification Text display an instruction to the player
    public void instruct(string instruction)
    {
        //This will be called with an input to designate what instruction to give the player
        switch (instruction)
        {
            case "defend":
                currText.text = "Pick which cards will defend";
                break;

            case "attack":
                currText.text = "Pick which cards will attack";
                break;

            default:
                break;
        }
    }
}
