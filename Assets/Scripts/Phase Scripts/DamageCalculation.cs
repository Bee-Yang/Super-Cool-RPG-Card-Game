using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void OnDisable()
    {
        /******************** Temporary Code ************************/
        ResetBlockCardColors();
        /******************** Temporary Code ************************/

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

    /******************** Temporary Code ************************/
    public void ResetBlockCardColors()
    {
        if (!turnController.IsPlayerTurn)
        {
            Transform blockField = GameObject.Find("PlayerPlayingField").transform;

            CardAttributes attributes;

            foreach (Transform card in blockField)
            {
                attributes = card.GetComponent<CardAttributes>();

                card.GetComponent<Outline>().enabled = false;

                card.Find("BlockingOrder").gameObject.SetActive(false);

                attributes.BlockOrder = 0;
            }
        }
    }
    /******************** Temporary Code ************************/
}
