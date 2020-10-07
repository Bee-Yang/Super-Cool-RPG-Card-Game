using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAttributes : MonoBehaviour
{
    private int id, cost, attack, health;
    private string cardName, type, description;
    private Sprite cardImage, cardBorder;

    public TMP_Text cardNameText, cardCostText, cardTypeText, cardDescriptionText, cardAttackText, cardHealthText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.cardNameText.text = this.cardName;
        this.cardCostText.text = "" + this.cost;
        this.cardTypeText.text = this.type;
        this.cardDescriptionText.text = this.description;
        this.cardAttackText.text = "" + this.attack;
        this.cardHealthText.text = "" + this.health;
        this.transform.Find("CardImage").transform.GetComponent<Image>().sprite = this.cardImage;
        this.transform.Find("CardBorder").transform.GetComponent<Image>().sprite = this.cardBorder;
    }

    public void SetName(string newName)
    {
        this.cardName = newName;
    }

    public void SetCost(int newCost)
    {
        this.cost = newCost;
    }

    public void SetType(string newType)
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

    public void SetImage(Sprite newSprite)
    {
        this.cardImage = newSprite;
    }

    public void SetBorder(Sprite newBorder)
    {
        this.cardBorder = newBorder;
    }
}
