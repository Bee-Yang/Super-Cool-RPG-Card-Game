using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckStart : MonoBehaviour
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
        decklist = Database.GetDeckByID(deckID);

        List<GameObject> card = new List<GameObject>();
        List<CardAttributes> cardAttribute = new List<CardAttributes>();

        for (int i = 0; i < decklist.cards.Count; ++i)
        {
            GameObject currCard = Instantiate(prefab, this.transform);
            card.Add(currCard);

            CardAttributes currAttributes = card[i].GetComponent<CardAttributes>();
            cardAttribute.Add(currAttributes);

            SetCardAttributes(cardAttribute[i], decklist.cards[i]);
            card[i].transform.Find("CardBack").transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetCardAttributes(CardAttributes card, Card cardData)
    {
        card.SetName(cardData.cardName);
        card.SetCost(cardData.cost);
        card.SetType(cardData.type);
        card.SetDescription(cardData.description);
        card.SetAttack(cardData.attack);
        card.SetHealth(cardData.health);
        card.SetImage(cardData.cardImage);
        card.SetBorder(cardData.cardBorder);
    }
}