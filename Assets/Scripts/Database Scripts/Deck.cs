using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Assets/Deck")]

// Deck list object for use in database
public class Deck : ScriptableObject
{
    public int id;
    public List<Card> cards;
}