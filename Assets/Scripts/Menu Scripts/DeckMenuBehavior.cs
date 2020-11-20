using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckMenuBehavior : MonoBehaviour
{
    private DeckChoiceBehavior deckChoice;

    void Start() {
        deckChoice = GameObject.Find("DeckChoice").GetComponent<DeckChoiceBehavior>();
    }

    public void Ready() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChooseNoble() {
        deckChoice.setDeckId(0);
        Debug.Log("Noble is chosen");
    }

     public void ChooseEvil() {
        deckChoice.setDeckId(1);
        Debug.Log("Evil is chosen");
    }

}
