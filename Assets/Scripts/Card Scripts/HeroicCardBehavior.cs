using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicCardBehavior : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private CardAttributes myAttributes;
    private CardBehavior myBehavior;
    private Transform playerField, enemyField;
    private int myID;
    private const int noble = 10, evil = 11;
    private bool effectUsed = false;


    // Start is called before the first frame update
    void Start()
    {
        turnController = this.GetComponent<TurnControllerBehavior>();
        myAttributes = this.gameObject.GetComponent<CardAttributes>();
        myBehavior = this.gameObject.GetComponent<CardBehavior>();
        playerField = GameObject.Find("PlayerPlayingField").transform;
        enemyField = GameObject.Find("OpponentPlayingField").transform;
        myID = myAttributes.GetID();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the effect has been used yet, and if the card is in play
        if (!effectUsed && myBehavior.IsInPlay())
        {
            //Check which heroic card is being played
            switch (myID)
            {
                case noble:
                    //If player turn, apply effect to player's field. Else apply effect to enemy's field
                    if (turnController.IsPlayerTurn)
                    { NobleEffect(playerField); }
                    else { NobleEffect(enemyField); }

                    break;

                case evil:
                    //If player turn, apply effect to player's field. Else apply effect to enemy's field
                    if (turnController.IsPlayerTurn)
                    { EvilEffect(enemyField); }
                    else { EvilEffect(playerField); }
                    break;

                default:
                    break;
            }

            //Set the effect to have been used already
            effectUsed = true;
        }

            
    }

    //Evil Heroic Card Effect:
    //Choose a card on the field of the opponent to the card owner at random, and destroy it (Heroic cards cannot be destroyed in this way)
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

    //Noble Heroic Card Effect:
    //All cards on the card owner's field have their attack and health increased by 1
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
