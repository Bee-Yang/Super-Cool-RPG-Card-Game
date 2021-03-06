﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject enlargedCard; // Enlarged clone of the card for hover
    private static float scale = 0.9f; // Scale for enlarged card

    private NotificationBehavior notificationBehavior; // To display when card is played

    private Transform currParent; // To save the parent to which this card is tied before/while being dragged

    // Status flags for the card
    private bool inPlay, draggable, hoverable, discardable, canAttack, attacking, canBlock, blocked, destroyed, targetable, notified;

    public bool Attacking {
        get { return this.attacking; }
        set { this.attacking = value; }
    }

    public bool Blocked {
        get { return this.blocked; }
        set { this.blocked = value; }
    }

    public bool Discardable {
        get { return this.discardable; }
        set { this.discardable = value; }
    }

    public bool Targetable {
        get { return this.targetable; }
        set { this.targetable = value; }
    }

    void Start()
    {
        //Set the current parent, set inPlay, destoryed, canAttack, attacking, canBlock, and blocked flags to false
        currParent = this.transform.parent;
        inPlay = false;
        destroyed = false;
        canAttack = false;
        attacking = false;
        canBlock = false;
        blocked = false;
        discardable = false;
        targetable = false;
        notified = false;

        notificationBehavior = GameObject.Find("NotificationText").GetComponent<NotificationBehavior>();
    }

    void Update()
    {
        // Check to see whether the card is destroyed or not
        CardAttributes card1 = this.gameObject.GetComponent<CardAttributes>();
        if (card1.GetCardType() != "Utility" && card1.GetCurrentHealth() <= 0)
        {
            this.destroyed = true;
            this.inPlay = false;
            this.canAttack = false;
            this.attacking = false;
            this.canBlock = false;
            this.blocked = false;
            this.notified = false;
            notificationBehavior.Destroy(card1);
        }

        // Set the color for the clone card to be the same as the card
        if (this.enlargedCard)
        {
            SetCloneColor();
            SetCloneAttributes();
        }

        //Check if card is in play, to update notification
        if (inPlay && !notified)
        {
            if (card1.GetCardType() == "Heroic")
            {
                notificationBehavior.HeroicCard(card1);
            }
            else
            {
                notificationBehavior.PlayCard(card1);
            }

            notified = true;
        }
    }

    //Flips the card to show face or back
    public void FlipCard(string face)
    {
        Image back = this.transform.Find("CardBack").transform.GetComponent<Image>();

        if (face.Equals("front"))
        {
            // Sets the CardBack to be transparent, "flipping" the card
            back.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else
        {
            // Sets the CardBack to be opaque, "flipping" the card
            back.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    //Sets the InPlay flag to true
    public void PutInPlay()
    {
        inPlay = true;
    }

    //Sets the InPlay flag to false
    public void PutOutOfPlay()
    {
        inPlay = false;
    }

    //Check if InPlay is true
    public bool IsInPlay()
    {
	return inPlay;
    }

    //Check if destroyed
    public bool IsDestroyed()
    {
	return destroyed;
    }

    //Set to destroyed
    public void SetDestroyed()
    {
	this.destroyed = true;
    }

    public Transform GetCurrParent()
    {
        return currParent;
    }

    public void SetCurrParent(Transform newParent)
    {
        currParent = newParent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (draggable == false) { eventData.pointerDrag = null;  } //Cancels drag event if card is not draggable

        else
        {
            this.transform.SetParent(this.transform.root); //Removes the card from its parent to the board while being dragged

            GetComponent<CanvasGroup>().blocksRaycasts = false; //Sets the card to not block raycasts, so panels can detect the pointer

            // Destroy clone card when card is dragged
            OnPointerExit();

            // Disable hover for all cards in player hand
            GameObject panel = GameObject.Find("PlayerHand");
            SetRaycastsInPanel(panel, false);

            // Disable hover for all cards in player playing field
            panel = GameObject.Find("PlayerPlayingField");
            SetRaycastsInPanel(panel, false);

            // Disable hover for all cards in opponent playing field
            panel = GameObject.Find("OpponentPlayingField");
            SetRaycastsInPanel(panel, false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position; //Makes the card follow the pointer
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(currParent); //Puts card into its current parent
        GetComponent<CanvasGroup>().blocksRaycasts = true; //Sets the card to block raycasts once more

        if (inPlay == true) { draggable = false; } //Set draggable to false if card is in play

        // Enable hover for all cards in player hand
        GameObject panel = GameObject.Find("PlayerHand");
        SetRaycastsInPanel(panel, true);

        // Enable hover for all cards in player playing field
        panel = GameObject.Find("PlayerPlayingField");
        SetRaycastsInPanel(panel, true);

        // Enable hover for all cards in opponent playing field
        panel = GameObject.Find("OpponentPlayingField");
        SetRaycastsInPanel(panel, true);
    }

    public void OnPointerEnter()
    {
        if (!hoverable) { return; } // Do nothing if the card is not hoverable

        // Create a clone card onto the board and disable all its behaviors
        enlargedCard = Instantiate(this.gameObject, this.transform.root, true);
        enlargedCard.GetComponent<CardBehavior>().enabled = false;
        enlargedCard.GetComponent<CanvasGroup>().blocksRaycasts = false;

        // Set the tag of the clone card to "Clone"
        enlargedCard.tag = "Clone";

        // Set the attributes of the clone card
        SetCloneAttributes();

        // Set the scale and position of the clone card
        enlargedCard.transform.localScale = new Vector3(scale, scale, 1.0f);

        if (this.tag == "Player")
        {
            enlargedCard.transform.localPosition += new Vector3(0.0f, 45.0f, 0.0f);
        }
        else
        {
            enlargedCard.transform.localPosition += new Vector3(0.0f, -45.0f, 0.0f);
            enlargedCard.transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f));
        }
    }

    public void OnPointerExit()
    {
        if (!hoverable) { return; } // Do nothing if the card is not hoverable

        // Get an array of all of the clone cards currently on the board
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

        // Destroy all clone cards on the board
        foreach(GameObject clone in clones)
        {
            Destroy(clone);
        }
    }

    public void OnClick()
    {
        // Check if the card is able to attack, block, or be discarded
        if (this.canAttack)
        {
            // Call the attack routine
            AttackClickRoutine();
        }
        else if (this.canBlock)
        {
            // Call the block routine
            BlockClickRoutine();
        }
        else if (this.discardable)
        {
            DiscardRoutine();
        }
        else if (this.targetable)
        {
            TargetClickRoutine();
        }
    }

    public void SetDraggable(bool status)
    {
        this.draggable = status;
    }

    public void SetHoverable(bool status)
    {
        this.hoverable = status;
    }

    public void SetCanAttack(bool status)
    {
        this.canAttack = status;
    }

    public void SetCanBlock(bool status)
    {
        this.canBlock = status;
    }

    public void SetRaycastsInPanel(GameObject panel, bool status)
    {
        // Check if the panel has no cards
        if (panel.transform.childCount > 0)
        {
            // Set the raycast to given status for all cards in the panel
            for (int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = status;
            }
        }
    }

    public void SetCloneAttributes()
    {
        CardAttributes clone, original;

        // Set values for CardAttributes of original and clone card
        original = this.gameObject.GetComponent<CardAttributes>();
        clone = enlargedCard.GetComponent<CardAttributes>();

        // Set the attributes for clone card
        clone.SetName(original.GetName());
        clone.SetCost(original.GetCost());
        clone.SetCardType(original.GetCardType());
        clone.SetDescription(original.GetDescription());
        clone.SetAttack(original.GetAttack());
        clone.SetHealth(original.GetCurrentHealth());
        clone.SetImage(original.GetImage());
        clone.SetBorder(original.GetBorder());
        clone.BlockOrder = original.BlockOrder;

        this.enlargedCard.transform.Find("BlockingOrder").gameObject.SetActive(this.transform.Find("BlockingOrder").gameObject.activeSelf);
    }

    public void SetCloneColor()
    {
        // Get the current color of the card
        Color currColor = this.transform.GetChild(0).GetComponent<Image>().color;

        // Change the color of the clone card to the current color of the card
        enlargedCard.transform.GetChild(0).GetComponent<Image>().color = currColor;
        enlargedCard.transform.GetChild(1).GetComponent<Image>().color = currColor;

        this.enlargedCard.GetComponent<Outline>().enabled = this.GetComponent<Outline>().enabled;
    }

    public void AttackClickRoutine()
    {
        if (!this.attacking)
        {
            // Set attacking to true
            this.attacking = true;

            // Make the card image brighter
            this.transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            this.transform.GetChild(1).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            // Set attacking to false
            this.attacking = false;

            // Make the card image darker
            this.transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            this.transform.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
    }

    public void BlockClickRoutine()
    {
        Defending block = GameObject.Find("TurnController").GetComponent<Defending>();

        if(!block.BlockingCards.Contains(this.gameObject))
        {
            // Select this card as the blocking card if there is no blocking card yet
            block.BlockingCards.Add(this.gameObject);

            this.GetComponent<CardAttributes>().BlockOrder = (block.BlockingCards.IndexOf(this.gameObject) + 1);
            this.transform.Find("BlockingOrder").gameObject.SetActive(true);
            this.GetComponent<Outline>().enabled = true;
        }
        else if(block.BlockingCards.Contains(this.gameObject))
        {
            // Deselect this card as the blocking card if it is the blocking card
            block.BlockingCards.Remove(this.gameObject);

            this.GetComponent<CardAttributes>().BlockOrder = 0;
            this.transform.Find("BlockingOrder").gameObject.SetActive(false);
            this.GetComponent<Outline>().enabled = false;

            foreach(GameObject card in block.BlockingCards)
            {
                card.GetComponent<CardAttributes>().BlockOrder = (block.BlockingCards.IndexOf(card) + 1);
            }
        }
    }

    public void DiscardRoutine()
    {
        this.destroyed = true;
        this.discardable = false;
    }

    public void TargetClickRoutine()
    {
        // Check if the target card is an enemy card
        if (this.tag == "Enemy")
        {
            // Temporary variables
            Transform field;
            CardBehavior behavior;

            // Assign the value for the AI's playing field
            field = GameObject.Find("OpponentPlayingField").transform;

            //Iterate through each card in the opponent's field
            foreach (Transform card in field)
            {
                // Assign the value for the behavior of the card
                behavior = card.GetComponent<CardBehavior>();

                // Check if the card's targetable status is true
                if (behavior.Targetable)
                {
                    // Make the card untargetable
                    behavior.Targetable = false;
                }
                else
                {
                    // Reset the color for the image of the card
                    card.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    card.GetChild(1).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
            }
        }

        // Assign the value for the effect target list
        EffectTargetList targetList = GameObject.Find("Utility").GetComponent<EffectTargetList>();

        // Add the card to the effect target list
        targetList.targets.Add(this.gameObject);

        // Set the target status to done
        targetList.targetDone = true;
    }
}