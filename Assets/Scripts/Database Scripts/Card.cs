using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Assets/Card")]


// Card Object for use in database
public class Card : ScriptableObject
{
    public int id;
    public string cardName;
    public int cost;
    public string type;
    [TextArea]
    public string description;
    public int attack;
    public int health;
    public Sprite cardImage;
    public Sprite cardBorder;
}
