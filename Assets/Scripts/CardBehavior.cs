﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    Transform currParent; //To save the parent to which this card is tied before/while being dragged
    CardStack currStack;  //To save the parent CardStack
    bool inPlay;
    bool draggable;
    bool destroyed;

    void Start()
    {
        //Set the current parent, set inPlay and destoryed flags to false
        currParent = this.transform.parent;
        currStack = this.transform.parent.GetComponent<CardStack>();
        inPlay = false;
        draggable = true;
        destroyed = false;
    }

    void Update()
    {
        //TODO
    }

    //Flips the card to show face or back
    public void FlipCard()
    {
        //Sets the CardBack to be transparent, "flipping" the card
        this.transform.Find("CardBack").transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    //Sets the InPlay flag
    public void PutInPlay()
    {
        inPlay = true;
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
            currStack.DrawCard(this); //Draw the card from its current CardStack
            Debug.Log(currStack.GetSize());
            this.transform.SetParent(this.transform.root); //Removes the card from its parent to the board while being dragged
            GetComponent<CanvasGroup>().blocksRaycasts = false; //Sets the card to not block raycasts, so panels can detect the pointer
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
        currStack = currParent.GetComponent<CardStack>(); //Sets the card's new CardStack
        currStack.AddCard(this); //Adds this card to the new CardStack's list
        Debug.Log(currStack.GetSize());

        if (inPlay == true) { draggable = false; } //Set draggable to false if card is in play
    }
}