using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Assets/Deck")]

public class Deck : ScriptableObject
{
    public int id;
    public List<Card> cards;
}