using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Database", menuName = "Assets/Databases/Card Database")]

public class CardDatabase : ScriptableObject
{
    public List<Card> allCards;
}
