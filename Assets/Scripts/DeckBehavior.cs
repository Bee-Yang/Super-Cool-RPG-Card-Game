using UnityEngine;

public class DeckBehavior : MonoBehaviour
{
    private Deck decklist;

    public GameObject prefab, hand;
    public int deckID;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        // Load decklist from the database
        decklist = Database.GetDeckByID(deckID);

        // Generate the deck onto the board
        DeckStart();

        // Shuffle the deck
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
        card.SetCardType(cardData.type);
        card.SetDescription(cardData.description);
        card.SetAttack(cardData.attack);
        card.SetHealth(cardData.health);
        card.SetImage(cardData.cardImage);
        card.SetBorder(cardData.cardBorder);
    }

    // Method for generating the deck onto the board from the database
    private void DeckStart()
    {
        // Declare temporary card gameobjects and cardAttribute Ojbects
        GameObject card;
        CardAttributes cardAttribute;

        for (int i = 0; i < decklist.cards.Count; ++i)
        {
            // Create the card onto the board
            card = Instantiate(prefab, this.transform);

            card.tag = this.tag;

            // Find the cardAttribute component in order to modify the card
            cardAttribute = card.GetComponent<CardAttributes>();

            // Set the attributes of the card
            SetCardAttributes(cardAttribute, decklist.cards[i]);

            // Make the card back image visible while the card is in the deck
            card.GetComponent<CardBehavior>().FlipCard("back");

            // Disable the card dragging script
            card.GetComponent<CardBehavior>().SetDraggable(false);
            card.GetComponent<CardBehavior>().SetHoverable(false);
        }
    }

    // Method for shuffling the deck
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

    public void Draw()
    {
        if (this.transform.childCount > 0)
        {
            // Get the index for the card at the top of the deck
            int top = (this.transform.childCount - 1);
            Transform card = this.transform.GetChild(top);

            /*
            // Enable hovering for the card if it is the player's card
            if (card.tag == "Player")
            {
                card.GetComponent<CardBehavior>().SetHoverable(true);
                card.GetComponent<CardBehavior>().FlipCard("front");
            }
            */

            card.GetComponent<CardBehavior>().SetHoverable(true);
            card.GetComponent<CardBehavior>().FlipCard("front");

            // Move the card from the deck into the hand
            card.SetParent(hand.transform);
            card.GetComponent<CardBehavior>().SetCurrParent(card.transform.parent);

            hand.GetComponent<HandSpacing>().SetHandSpacing();
        }
    }
}