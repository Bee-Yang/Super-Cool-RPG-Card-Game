using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardStack : MonoBehaviour
{
    List<CardBehavior> cards = new List<CardBehavior>();

    // Start is called before the first frame update
    void Start()
    {
        //For every child in the card stack, add it to the card list. (This is entirely for the deck)
        for (int i = 0; i <  this.gameObject.transform.childCount; i++)
        {
            CardBehavior card = this.gameObject.transform.GetChild(i).GetComponent<CardBehavior>();
            cards.Add(card);
        }
        Debug.Log(this.GetSize());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetSize()
    {
        return cards.Count;
    }

    public void AddCard(CardBehavior card)
    {
        cards.Add(card);
    }

    public void DrawCard(CardBehavior card)
    {
        cards.Remove(card); //Removes the drawn card from the card list
    }

    //Shuffles the order of the cards
    public void Shuffle()
    {
        //TODO
    }
}
