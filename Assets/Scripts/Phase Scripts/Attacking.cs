using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private BattleController battleController;
    private GameObject AI;
    private Transform playingField;

    void Awake()
    {
        // Assign the controllers
        turnController = this.GetComponent<TurnControllerBehavior>();
        battleController = this.GetComponent<BattleController>();

        AI = GameObject.Find("AI");
    }

    void OnEnable()
    {
        // Find the playing field of the current player
        if (turnController.IsPlayerTurn)
        {
            playingField = GameObject.Find("PlayerPlayingField").transform;

            GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = true;
        }
        else
        {
            playingField = GameObject.Find("OpponentPlayingField").transform;
        }

        // Check if the player has creatures in play
        if (playingField.childCount > 0)
        {
            Transform card;

            // For loop to go through all cards on the playing field
            for (int i = 0; i < playingField.childCount; i++)
            {
                // Get the card at index i
                card = playingField.GetChild(i);

                if (turnController.IsPlayerTurn)
                {
                    // Set the canAttack status of the card to true
                    card.GetComponent<CardBehavior>().SetCanAttack(true);
                }

                // Make the image of the card darker
                card.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                card.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
        }
    }

    void OnDisable()
    {
        // Find the playing field of the current player
        if (turnController.IsPlayerTurn)
        {
            // Check if the player has creatures in play
            if (playingField.childCount > 0)
            {
                Transform card;

                // For loop to go through all cards on the playing field
                for (int i = 0; i < playingField.childCount; i++)
                {
                    // Get the card at index i
                    card = playingField.GetChild(i);

                    // Set the canAttack status of the card to false
                    card.GetComponent<CardBehavior>().SetCanAttack(false);
                }
            }

            // Disable the end turn button
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
        }

        if (AI.GetComponent<AIAttackScript>().enabled)
        {
            AI.GetComponent<AIAttackScript>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the attacking player has creatures in play
        if (playingField.childCount == 0)
        {
            // Automatically end the player's turn
            battleController.SetPhase(0);

            // Disable this script
            this.enabled = false;
        }
        // Check if the AI is the one attacking
        else if (!turnController.IsPlayerTurn)
        {
            // Do AI attacking phase routine
            AIAttackRoutine();
        }
    }

    public void AIAttackRoutine()
    {
        // Check if the AI playing phase script is enabled
        if (!AI.GetComponent<AIAttackScript>().enabled)
        {
            // Enable the AI playing phase script
            AI.GetComponent<AIAttackScript>().enabled = true;
        }
        // Check if the AI is done with the playing phase
        else if (AI.GetComponent<AIAttackScript>().Done)
        {
            // Disable the AI playing phase script and go into the battle phase
            AI.GetComponent<AIAttackScript>().enabled = false;
            battleController.SetPhase(2);

            this.enabled = false;
        }
    }
}