using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Murder : MonoBehaviour
{
    private AITargeting AITarget;
    private TurnControllerBehavior turnController;
    private EffectTargetList targetList;
    private Transform playerField, opponentField;
    CardAttributes attributes;
    CardBehavior behavior;
    private bool effectUsed;

    void Awake()
    {
        this.AITarget = GameObject.Find("AI").GetComponent<AITargeting>();
        this.turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        this.targetList = GameObject.Find("Utility").GetComponent<EffectTargetList>();
        this.playerField = GameObject.Find("PlayerPlayingField").transform;
        this.opponentField = GameObject.Find("OpponentPlayingField").transform;
        this.effectUsed = false;
    }

    void OnEnable()
    {
        this.targetList.enabled = true;

        if (this.turnController.IsPlayerTurn)
        {
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
            GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = false;

            //Iterate through each card in the opponent's field
            foreach (Transform card in this.opponentField)
            {
                this.attributes = card.GetComponent<CardAttributes>();

                if (this.attributes.GetCurrentHealth() <= 3)
                {
                    this.behavior = card.GetComponent<CardBehavior>();

                    this.behavior.Targetable = true;
                }
                else
                {
                    // Make the image of the card darker
                    card.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    card.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                }
            }
        }
        else
        {
            //Iterate through each card in the opponent's field
            foreach (Transform card in this.playerField)
            {
                this.attributes = card.GetComponent<CardAttributes>();

                if (this.attributes.GetCurrentHealth() <= 3)
                {
                    this.targetList.targets.Add(card.gameObject);
                }
            }

            this.AITarget.enabled = true;
        }
    }

    void OnDisable()
    {
        if (this.turnController.IsPlayerTurn)
        {
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = true;
            GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = true;
        }
        else if (AITarget.enabled)
        {
            AITarget.enabled = false;
        }

        this.effectUsed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.targetList.targetDone && !this.effectUsed )
        {
            this.effectUsed = true;

            foreach(GameObject card in this.targetList.targets)
            {
                this.behavior = card.GetComponent<CardBehavior>();

                this.behavior.SetDestroyed();
            }

            this.targetList.effectDone = true;

            this.enabled = false;
        }
    }
}
