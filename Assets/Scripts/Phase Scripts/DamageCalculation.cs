﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCalculation : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private BattleController battleController;
    private Defending cardData;

    void Awake()
    {
        // Assign the controllers
        turnController = this.GetComponent<TurnControllerBehavior>();
        battleController = this.GetComponent<BattleController>();
        cardData = this.GetComponent<Defending>();
    }

    void OnEnable()
    {
        Debug.Log("Damage Calculation");

        int attackValue = cardData.CurrentAttackCard.GetComponent<CardAttributes>().GetAttack();

        //Iterate through each defending card, decrementing their health and decrementing attackValue
        foreach (GameObject card in cardData.BlockingCards)
        {
            if (attackValue > 0)
            {
                attackValue = DefendAttack(attackValue, card);
            }

            card.GetComponent<CardBehavior>().Blocked = true;
        }

        //Check for spillover damage to defending player's health
        if (attackValue > 0)
        {
            if (turnController.IsPlayerTurn)
            {
                GameObject.Find("EnemyHealth").GetComponent<HealthBehavior>().DecreaseHealth(attackValue);
            }
            else
            {
                GameObject.Find("PlayerHealth").GetComponent<HealthBehavior>().DecreaseHealth(attackValue);
            }
        }

        battleController.SetPhase(2);

    }

    void OnDisable()
    {
        ResetBlockCardColors();
    }

    void Update()
    {
        battleController.SetPhase(0);
    }

    /******************** Temporary Code ************************/
    public void ResetBlockCardColors()
    {
        if (!turnController.IsPlayerTurn)
        {
            Transform blockField = GameObject.Find("PlayerPlayingField").transform;

            CardAttributes attributes;

            foreach (Transform card in blockField)
            {
                attributes = card.GetComponent<CardAttributes>();

                card.GetComponent<Outline>().enabled = false;

                card.Find("BlockingOrder").gameObject.SetActive(false);

                attributes.BlockOrder = 0;
            }
        }
    }
    /******************** Temporary Code ************************/

    int DefendAttack(int attack, GameObject defender)
    {
        int defenderHealth = defender.GetComponent<CardAttributes>().GetCurrentHealth();

        //If the attack would kill the defender, set defender health to 0
        if (attack > defenderHealth)
        {
            defender.GetComponent<CardAttributes>().SetCurrentHealth(0);
        }

        //Else, set the defender's health to reflect damage taken
        else
        {
            defender.GetComponent<CardAttributes>().SetCurrentHealth(defenderHealth - attack);
        }

        //Return what's left of the attack
        return attack - defenderHealth;
    }
}
