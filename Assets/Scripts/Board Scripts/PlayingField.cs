using UnityEngine;
using UnityEngine.EventSystems;

public class PlayingField : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private PlayingPhase playPhase;

    void Start()
    {
        this.playPhase = GameObject.Find("TurnController").GetComponent<PlayingPhase>();
    }

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
        CardAttributes attributes = eventData.pointerDrag.GetComponent<CardAttributes>(); //Gets the attributeds card that was dropped

        if (card != null)
        {
            // Check if card can be played
            if (playPhase.CardCanBePlayed(card.gameObject))
            {
                if (attributes.GetCardType() == "Utility")
                {
                    card.SetCurrParent(this.transform.root);
                    card.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else
                {
                    card.SetCurrParent(this.transform); //Sets the card's current parent to the player's field
                }
                card.PutInPlay(); //Sets the cards inPlay flag to true
            }
        }
    }
}
