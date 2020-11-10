using UnityEngine;

public class BattleController : MonoBehaviour
{
    public int phase;
    public TurnControllerBehavior turnController;
    public const int attacking = 1, defending = 2, dmgCalc = 3;

    void OnEnable()
    {
        turnController = transform.GetComponent<TurnControllerBehavior>();
        this.phase = 1;
    }

    void OnDisable()
    {
        resetBlocks();
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

    public void resetBlocks()
    {
        if (turnController.IsPlayerTurn)
        {
            foreach (Transform card in GameObject.Find("OpponentPlayingField").transform)
            {
                card.GetComponent<CardBehavior>().Blocked = false;
            }
        }
        else
        {
            foreach (Transform card in GameObject.Find("PlayerPlayingField").transform)
            {
                card.GetComponent<CardBehavior>().Blocked = false;
            }
        }
    }
}