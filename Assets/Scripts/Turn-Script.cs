using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Turn-Script : MonoBehaviour
{
        Public bool switchTurn = true;
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
    Public void AlternateTurn()
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
