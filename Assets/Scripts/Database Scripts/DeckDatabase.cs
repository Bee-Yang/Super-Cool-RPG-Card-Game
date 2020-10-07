using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck Database", menuName = "Assets/Databases/Deck Database")]

public class DeckDatabase : ScriptableObject
{
    public List<Deck> allDecks;
}
