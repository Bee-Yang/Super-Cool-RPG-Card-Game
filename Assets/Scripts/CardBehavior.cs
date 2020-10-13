using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject enlargedCard;
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

            // Destroy clone card when card is dragged
            OnPointerExit();

            // Disable hover for all cards in player hand
            GameObject panel = GameObject.Find("PlayerHand");
            SetHoverAndRaycastsInPanel(panel, false);

            // Disable hover for all cards in player playing field
            panel = GameObject.Find("PlayerPlayingField");
            SetHoverAndRaycastsInPanel(panel, false);

            // Disable hover for all cards in opponent playing field
            panel = GameObject.Find("OpponentPlayingField");
            SetHoverAndRaycastsInPanel(panel, false);
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
        SetHoverAndRaycastsInPanel(panel, true);

        // Enable hover for all cards in player playing field
        panel = GameObject.Find("PlayerPlayingField");
        SetHoverAndRaycastsInPanel(panel, true);

        // Enable hover for all cards in opponent playing field
        panel = GameObject.Find("OpponentPlayingField");
        SetHoverAndRaycastsInPanel(panel, true);
    }

    public void OnPointerEnter()
    {
        if (!hoverable) { return; }

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
        enlargedCard.transform.localPosition += new Vector3(0.0f, 25.0f, 0.0f);
    }

    public void OnPointerExit()
    {
        if (!hoverable) { return; }

        // Get an array of all of the clone cards currently on the board
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

        // Destroy all clone cards on the board
        foreach(GameObject clone in clones)
        {
            Destroy(clone);
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

    public void SetHoverAndRaycastsInPanel(GameObject panel, bool status)
    {
        // Check if the panel has no cards
        if (panel.transform.childCount > 0)
        {
            // Set the hoverable and raycast to given status for all cards in the panel
            for (int i = 0; i < panel.transform.childCount; ++i)
            {
                panel.transform.GetChild(i).GetComponent<CardBehavior>().SetHoverable(status);
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
    }
}