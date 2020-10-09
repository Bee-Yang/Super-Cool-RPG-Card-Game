using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private static float scale = 0.8f;
    private Vector3 originalScale;

    private Transform currParent; //To save the parent to which this card is tied before/while being dragged
    bool inPlay;
    bool draggable;
    bool hoverable;
    bool destroyed;

    void Start()
    {
        //Set the current parent, set inPlay and destoryed flags to false
        currParent = this.transform.parent;
        inPlay = false;
        destroyed = false;

        this.originalScale = this.transform.localScale;
    }

    void Update()
    {
        //TODO
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
            this.transform.SetParent(this.transform.root); //Removes the card from its parent to the board while being dragged
            GetComponent<CanvasGroup>().blocksRaycasts = false; //Sets the card to not block raycasts, so panels can detect the pointer
        }

        GameObject panel = GameObject.Find("Hand-Player");

        if(panel.transform.childCount > 0)
        {
            for(int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CardBehavior>().SetHoverable(false);
            }
        }

        panel = GameObject.Find("PlayerPlayingField");

        if (panel.transform.childCount > 0)
        {
            for (int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CardBehavior>().SetHoverable(false);
            }
        }

        panel = GameObject.Find("OpponentPlayingField");

        if (panel.transform.childCount > 0)
        {
            for (int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CardBehavior>().SetHoverable(false);
            }
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

        GameObject panel = GameObject.Find("Hand-Player");

        if (panel.transform.childCount > 0)
        {
            for (int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CardBehavior>().SetHoverable(true);
            }
        }

        panel = GameObject.Find("PlayerPlayingField");

        if (panel.transform.childCount > 0)
        {
            for (int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CardBehavior>().SetHoverable(true);
            }
        }

        panel = GameObject.Find("OpponentPlayingField");

        if (panel.transform.childCount > 0)
        {
            for (int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CardBehavior>().SetHoverable(true);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hoverable) { return; }

        // Enlarge the card and change its position to fit on the screen
        this.transform.localScale = new Vector3(scale, scale, 1.0f);
        this.transform.localPosition += new Vector3(0.0f, 25.0f, 0.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!hoverable) { return; }

        // Restore scale and position of the card
        if (!this.transform.localScale.Equals(originalScale))
        {
            this.transform.localScale = originalScale;
            this.transform.localPosition -= new Vector3(0.0f, 25.0f, 0.0f);
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
}