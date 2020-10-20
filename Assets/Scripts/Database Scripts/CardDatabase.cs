using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Database", menuName = "Assets/Databases/Card Database")]

// Card database object containing a list of all cards
public class CardDatabase : ScriptableObject
{
    public List<Card> allCards;
}
