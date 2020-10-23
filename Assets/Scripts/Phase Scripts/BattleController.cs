using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public int phase;
    public const int attacking = 1, defending = 2, dmgCalc = 3;

    void OnEnable()
    {
        Debug.Log("Battle Controller Enabled.");

        this.phase = 1;
    }

    void OnDisable()
    {
        this.DisableAllPhases();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
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

    public int GetPhase()
    {
        return this.phase;
    }

    public void SetPhase(int newPhase)
    {
        this.phase = newPhase;
    }

    public void DisableAllPhases()
    {
        this.GetComponent<Attacking>().enabled = false;
        this.GetComponent<Defending>().enabled = false;
        this.GetComponent<DamageCalculation>().enabled = false;
    }
}
