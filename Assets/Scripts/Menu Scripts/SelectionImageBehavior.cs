using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionImageBehavior : MonoBehaviour
{
    public Image nobleImage;
    public Image evilImage;

    private DeckChoiceBehavior deckChoice;
    // Start is called before the first frame update
    void Start()
    {
        deckChoice = GameObject.Find("DeckChoice").GetComponent<DeckChoiceBehavior>();
        nobleImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (deckChoice.getDeckId() == 0) {
            nobleImage.enabled = true;
            evilImage.enabled = false;
        }

        if (deckChoice.getDeckId() == 1) {
            nobleImage.enabled = false;
            evilImage.enabled = true;
        }
    }
}
