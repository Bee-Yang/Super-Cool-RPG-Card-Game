using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Database : MonoBehaviour
{
    public CardDatabase cards;
    public DeckDatabase decks;

    public static Database instance;

    private void Awake()
    {
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
        return instance.cards.allCards.FirstOrDefault(i => i.id == ID);
    }

    public static Deck GetDeckByID(int ID)
    {
        return instance.decks.allDecks.FirstOrDefault(i => i.id == ID);
    }
}
