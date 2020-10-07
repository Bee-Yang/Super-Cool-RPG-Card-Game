using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerBehavior : MonoBehaviour
{
    private bool isPlayerTurn;
    public bool IsPlayerTurn {
    get { return isPlayerTurn; }
    set { isPlayerTurn = value; }
}
    void Start()
    {
        isPlayerTurn = true;
        playerGo();
    }

    void Update()
    {
        if (isPlayerTurn == true) {
            playerGo();
        } 
        else {
            opponentGo();
        }
    }
    public void AlternateTurn() {
        isPlayerTurn = !isPlayerTurn;
    }

    void playerGo()
    {
        //phases will be controlled here, turn ended manually by button
    }
    void opponentGo()
    {
        //phases will be controlled here, turn ended automatically
        AlternateTurn();
    }

}