using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckChoiceBehavior : MonoBehaviour
{
    private int playerDeckID = 0;
    private int enemyDeckID = 0;

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
        playerDeckID = id;
        
        if (id == 0)
        {
            enemyDeckID = 1;
        }
        else
        {
            enemyDeckID = 0;
        }

        Debug.Log("Deck id is " + id);
    }

    public int getPlayerDeckID() {
        return playerDeckID;
    }

    public int getEnemyDeckID() {
        return enemyDeckID;
    }
}
