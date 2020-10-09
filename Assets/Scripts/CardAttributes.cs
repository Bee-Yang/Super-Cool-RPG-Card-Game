using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAttributes : MonoBehaviour
{
    private int id, cost, attack, health, currentHealth;
    private string cardName, type, description;
    private Sprite cardImage, cardBorder;

    public TMP_Text cardNameText, cardCostText, cardTypeText, cardDescriptionText, cardAttackText, cardHealthText;

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = this.health;
    }

    // Update is called once per frame
    void Update()
    {
        this.cardNameText.text = this.cardName;
        this.cardCostText.text = "" + this.cost;
        this.cardTypeText.text = this.type;
        this.cardDescriptionText.text = this.description;
        this.cardAttackText.text = "" + this.attack;
        this.cardHealthText.text = "" + this.currentHealth;
        this.transform.Find("CardImage").transform.GetComponent<Image>().sprite = this.cardImage;
        this.transform.Find("CardBorder").transform.GetComponent<Image>().sprite = this.cardBorder;
    }

    public string GetName()
    {
        return this.cardName;
    }

    public int GetCost()
    {
        return this.cost;
    }

    public string GetCardType()
    {
        return this.type;
    }

    public string GetDescription()
    {
        return this.description;
    }

    public int GetAttack()
    {
        return this.attack;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }

    public void SetName(string newName)
    {
        this.cardName = newName;
    }

    public void SetCost(int newCost)
    {
        this.cost = newCost;
    }

    public void SetCardType(string newType)
    {
        this.type = newType;
    }

    public void SetDescription(string newDescription)
    {
        this.description = newDescription;
    }

    public void SetAttack(int newAttack)
    {
        this.attack = newAttack;
    }

    public void SetHealth(int newHealth)
    {
        this.health = newHealth;
    }

    public void SetCurrentHealth(int newHealth)
    {
        this.currentHealth = newHealth;
    }

    public void SetImage(Sprite newSprite)
    {
        this.cardImage = newSprite;
    }

    public void SetBorder(Sprite newBorder)
    {
        this.cardBorder = newBorder;
    }
}
