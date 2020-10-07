using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TurnScript : MonoBehaviour
{
    private boolean switchTurn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTurn == true)
        {
            playerGo();
        } else
        {
            opponentGo();
        }
    }
    public void AlternateTurn()
    {
        switchTurn = !switchTurn;
    }
    void playerGo()
    {

    }
    void opponentGo()
    {

    }

}
