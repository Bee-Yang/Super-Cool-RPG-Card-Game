using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rally : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private EffectTargetList targetList;
    private Transform playerField;
    private Transform opponentField;
    private CardAttributes attributes;

    private void Awake()
    {
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        targetList = GameObject.Find("Utility").GetComponent<EffectTargetList>();
        playerField = GameObject.Find("PlayerPlayingField").transform;
        opponentField = GameObject.Find("OpponentPlayingField").transform;
    }

    private void OnEnable()
    {
        this.targetList.enabled = true;
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Check if it is the player's turn or the opponent's turn
        if (turnController.IsPlayerTurn)
        {
            //Iterate through each card in the player's field
            foreach (Transform child in playerField)
            {
                attributes = child.GetComponent<CardAttributes>();

                //If it is not a Utility card, increase its attack by 1
                if (attributes.GetCardType() != "Utility")
                {
                    attributes.SetAttack(attributes.GetAttack() + 1);
                }
            }
        }
        else
        {
            //Iterate through each card in the opponent's field
            foreach (Transform child in opponentField)
            {
                attributes = child.GetComponent<CardAttributes>();

                //If it is not a Utility card, increase its attack by 1
                if (attributes.GetCardType() != "Utility")
                {
                    attributes.SetAttack(attributes.GetAttack() + 1);
                }
            }
        }

        targetList.effectDone = true;

        // Disable the Rally script
        this.enabled = false;
    }
}
