﻿using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //Pointer enter and exit are required for OnDrop to work
    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        CardBehavior card = eventData.pointerDrag.GetComponent<CardBehavior>(); //Gets the card that was dropped
        if (card != null)
        {
            card.SetCurrParent(this.transform); //Sets the card's current parent to the player hand
        }
    }
}
