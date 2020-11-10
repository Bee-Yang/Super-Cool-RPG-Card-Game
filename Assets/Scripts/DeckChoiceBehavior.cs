using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckChoiceBehavior : MonoBehaviour
{
    private int deckId = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad( this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDeckId(int id) {
        deckId = id;
        Debug.Log("Deck id is " + id);
    }

    public int getDeckId() {
        return deckId;
    }
}
