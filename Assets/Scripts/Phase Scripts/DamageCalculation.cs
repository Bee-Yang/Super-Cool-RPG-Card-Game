using System.Collections.Generic;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private BattleController battleController;
    private Defending cardData;

    void Awake()
    {
        // Assign the controllers
        turnController = this.GetComponent<TurnControllerBehavior>();
        battleController = this.GetComponent<BattleController>();
        cardData = this.GetComponent<Defending>();
    }

    void OnEnable()
    {
        Debug.Log("Damage Calculation");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /**************** Temporary Code ******************/
        battleController.SetPhase(0);
        /**************** Temporary Code ******************/
    }
}
