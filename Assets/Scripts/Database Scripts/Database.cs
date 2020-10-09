using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Database : MonoBehaviour
{
    // Load card and deck database
    public CardDatabase cards;
    public DeckDatabase decks;

    public static Database instance;

    private void Awake()
    {
        // Create database instance if there is no active database
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Card GetCardByID(int ID)
    {
        // return the card from the card database that matches the given ID
        return instance.cards.allCards.FirstOrDefault(i => i.id == ID);
    }

    public static Deck GetDeckByID(int ID)
    {
        // return the decklist from the deck database that matches the given ID
        return instance.decks.allDecks.FirstOrDefault(i => i.id == ID);
    }
}
