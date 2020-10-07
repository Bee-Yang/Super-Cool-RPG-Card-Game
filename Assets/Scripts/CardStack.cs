using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardStack : MonoBehaviour
{
<<<<<<< Updated upstream
    List<Card> cards = new List<Card>();
=======
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        //For every child in the card stack, add it to the card list. (This is entirely for the deck)
        for (int i = 0; i <  this.gameObject.transform.childCount; i++)
        {
            Card card = this.gameObject.transform.GetChild(i).GetComponent<Card>();
            cards.Add(card);
        }
        Debug.Log(this.GetSize());
=======

>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

<<<<<<< Updated upstream
    public int GetSize()
    {
        return cards.Count;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void DrawCard(Card card)
    {
        cards.Remove(card); //Removes the drawn card from the card list
    }

=======
>>>>>>> Stashed changes
    //Shuffles the order of the cards
    public void Shuffle()
    {
        //TODO
    }
}