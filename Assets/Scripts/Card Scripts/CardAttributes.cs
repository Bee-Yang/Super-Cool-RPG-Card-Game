using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAttributes : MonoBehaviour
{
    private int id, cost, attack, health, currentHealth, blockOrder;
    private string cardName, type, description;
    private Sprite cardImage, cardBorder;

    public TMP_Text cardNameText, cardCostText, cardTypeText, cardDescriptionText, cardAttackText, cardHealthText, blockOrderText;

    public int BlockOrder{
        get { return this.blockOrder; }
        set { this.blockOrder = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = this.health;
        this.blockOrder = 0;
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
        this.blockOrderText.text = "" + this.blockOrder;
        this.transform.Find("CardImage").transform.GetComponent<Image>().sprite = this.cardImage;
        this.transform.Find("CardBorder").transform.GetComponent<Image>().sprite = this.cardBorder;
    
	    CardBehavior card = this.GetComponent<CardBehavior>();
	    // if card is destroyed, move to graveyard
	    if(card.IsDestroyed() == true) {
		
		    TurnControllerBehavior turnController = this.GetComponent<TurnControllerBehavior>();
		    // check for who's card it is and move it to their graveyard
		    if (this.tag == "Player")
		    {
                Transform Graveyard = GameObject.Find("PlayerGraveyard").transform;
                card.transform.SetParent(Graveyard);
			    card.PutOutOfPlay();
		    }
		    else
		    {
			    Transform Graveyard = GameObject.Find("OpponentGraveyard").transform;
                card.transform.SetParent(Graveyard);
                card.PutOutOfPlay();
		    }

            // Restore the card's colors
            card.transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            card.transform.GetChild(1).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            card.GetComponent<Outline>().enabled = false;
            card.transform.Find("BlockingOrder").gameObject.SetActive(false);
            BlockOrder = 0;
        }
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

    public Sprite GetImage()
    {
        return this.cardImage;
    }

    public Sprite GetBorder()
    {
        return this.cardBorder;
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
