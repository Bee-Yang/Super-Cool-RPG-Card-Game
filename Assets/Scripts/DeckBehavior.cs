using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DeckBehavior : MonoBehaviour
{
    private Deck decklist;

    public GameObject prefab;
    public int deckID;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        // Load decklist from the database
        decklist = Database.GetDeckByID(deckID);

        // Create list of card gameobjects and cardAttribute Ojbects
        List<GameObject> card = new List<GameObject>();
        List<CardAttributes> cardAttribute = new List<CardAttributes>();

        for (int i = 0; i < decklist.cards.Count; ++i)
        {
            // Create the card onto the board
            GameObject currCard = Instantiate(prefab, this.transform);
            card.Add(currCard);

            // Find the cardAttribute component in order to modify the card
            CardAttributes currAttributes = card[i].GetComponent<CardAttributes>();
            cardAttribute.Add(currAttributes);

            // Set the attributes of the card
            SetCardAttributes(cardAttribute[i], decklist.cards[i]);

            // Make the card back image visible while the card is in the deck
            //card[i].transform.Find("CardBack").transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetCardAttributes(CardAttributes card, Card cardData)
    {
        // Set attributes of the card based on the card data retrieved from the database
        card.SetName(cardData.cardName);
        card.SetCost(cardData.cost);
        card.SetType(cardData.type);
        card.SetDescription(cardData.description);
        card.SetAttack(cardData.attack);
        card.SetHealth(cardData.health);
        card.SetImage(cardData.cardImage);
        card.SetBorder(cardData.cardBorder);
    }

    public void Shuffle()
    {
        // Random num generator
        System.Random rnd = new System.Random();

        // Pick a random card, swap it with the card at position n of the deck
        int n = this.transform.childCount;
        while (n > 1)
        {
            // Updating the card positions
            n--;
            int k = rnd.Next(n + 1);

            // Pull a random card from the deck
            Transform tmp = this.transform.GetChild(k);
            
            // Swap the random card with the card at position n
            this.transform.GetChild(n).SetSiblingIndex(k);
            tmp.SetSiblingIndex(n);
        }
    }
}