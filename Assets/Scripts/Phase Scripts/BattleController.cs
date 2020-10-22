using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public int phase;
    public const int attacking = 1, defending = 2, dmgCalc = 3;

    // Start is called before the first frame update
    void Start()
    {
        phase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            // Enable the script for drawing 5 cards at the beginning of the duel
            case attacking:
                this.GetComponent<Attacking>().enabled = true;
                break;

            // Enable the script for drawing phase
            case defending:
                this.GetComponent<Defending>().enabled = true;
                break;

            case dmgCalc:
                this.GetComponent<DamageCalculation>().enabled = true;
                break;

            default:
                DisableAllPhases();
                break;
        }
    }

    void DisableAllPhases()
    {
        this.GetComponent<Attacking>().enabled = false;
        this.GetComponent<Defending>().enabled = false;
        this.GetComponent<DamageCalculation>().enabled = false;
    }
}
