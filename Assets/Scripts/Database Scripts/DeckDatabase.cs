using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck Database", menuName = "Assets/Databases/Deck Database")]


// Deck database containing a list of all decks
public class DeckDatabase : ScriptableObject
{
    public List<Deck> allDecks;
}
