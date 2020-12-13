using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageCalculation : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private NotificationBehavior notificationBehavior;
    private BattleController battleController;
    private Defending cardData;

    private bool handling, done;

    void Awake()
    {
        // Assign the controllers
        turnController = this.GetComponent<TurnControllerBehavior>();
        notificationBehavior = this.GetComponent<NotificationBehavior>();
        battleController = this.GetComponent<BattleController>();
        cardData = this.GetComponent<Defending>();
    }

    void OnEnable()
    {
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

                notificationBehavior.Attack(cardData.CurrentAttackCard.GetComponent<CardAttributes>(), card.GetComponent<CardAttributes>());
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

    int DefendAttack(int attack, GameObject defender)
    {
        int remainingAttack;
        int defenderHealth = defender.GetComponent<CardAttributes>().GetCurrentHealth();
        int defenderAttack = defender.GetComponent<CardAttributes>().GetAttack();
        int attackerHealth = cardData.CurrentAttackCard.GetComponent<CardAttributes>().GetCurrentHealth();

        // If the attack would kill the defender, set defender health to 0
        if (attack > defenderHealth)
        {
            StartCoroutine(ShowDamage(defender));
            defender.GetComponent<CardAttributes>().SetCurrentHealth(0);
        }

        // Else, set the defender's health to reflect damage taken
        else
        {
            StartCoroutine(ShowDamage(defender));
            defender.GetComponent<CardAttributes>().SetCurrentHealth(defenderHealth - attack);
        }

        // If the defender would kill the attacker, set the attacker health to 0 and set the remaining attack to 0
        if (defenderAttack >= attackerHealth)
        {

            StartCoroutine(ShowDamage(cardData.CurrentAttackCard));
            cardData.CurrentAttackCard.GetComponent<CardAttributes>().SetCurrentHealth(0);
            remainingAttack = 0;
        }

        // Else, set the attacker's health to reflect the damage taken and set the remaining attack to reflect the correct amount
        else
        {

            StartCoroutine(ShowDamage(cardData.CurrentAttackCard));
            cardData.CurrentAttackCard.GetComponent<CardAttributes>().SetCurrentHealth(attackerHealth - defenderAttack);
            remainingAttack = attack - defenderHealth;
        }

        RestoreCardColors(defender);

        //Return what's left of the attack
        return remainingAttack;
    }

    IEnumerator ShowDamage(GameObject card)
    {
        TMP_Text healthTextTMP = card.transform.Find("CardBorder").Find("CardHealth").gameObject.GetComponent<TMP_Text>();

        healthTextTMP.color = Color.red;
        healthTextTMP.fontSize = 55;

        yield return new WaitForSecondsRealtime(1);

        healthTextTMP.fontSize = 40;
    }

    public void RestoreCardColors(GameObject card)
    {
        // Restore the card's colors
        card.transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        card.transform.GetChild(1).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        card.GetComponent<Outline>().enabled = false;
        card.transform.Find("BlockingOrder").gameObject.SetActive(false);
        card.GetComponent<CardAttributes>().BlockOrder = 0;
    }
}
