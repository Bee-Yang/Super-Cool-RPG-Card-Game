using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCalculation : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private BattleController battleController;
    private Defending cardData;

    private bool handling, done;

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

        this.handling = false;
        this.done = false;
    }

    void OnDisable()
    {
        ResetBlockCardColors();
    }

    void Update()
    {
        turnController.CheckGameOverConditions();

        if (!this.handling)
        {
            this.handling = true;

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

            cardData.CurrentAttackCard.GetComponent<CardBehavior>().Attacking = false;

            cardData.CurrentAttackCard.GetComponent<Outline>().enabled = false;

            cardData.CurrentAttackCard.transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            cardData.CurrentAttackCard.transform.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

            this.done = true;
        }
        else if (this.done)
        {
            battleController.DisableAllPhases();
            battleController.SetPhase(2);
            this.enabled = false;
        }
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
}
