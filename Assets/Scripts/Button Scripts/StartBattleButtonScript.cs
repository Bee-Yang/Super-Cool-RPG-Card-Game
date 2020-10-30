using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartBattleButtonScript : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private BattleController battleController;

    public TMP_Text buttonText;

    void Awake()
    {
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        battleController = GameObject.Find("TurnController").GetComponent<BattleController>();

        // Make the button not clickable
        this.GetComponent<Button>().interactable = false;
    }

    void Start ()
    {
	}

    void Update()
    {
        if (turnController.GetPhase() == 3 && this.GetComponent<Button>().interactable == true)
        {
            this.buttonText.text = "End Selection";
        }
        else
        {
            this.buttonText.text = "Start Battle";
        }
    }

    public void TaskOnClick()
    {
        // Disable the start battle button
        this.GetComponent<Button>().interactable = false;

        // Check which phase it is
        if (turnController.GetPhase() == 2)
        {
            turnController.DisableAllPhases();
            turnController.SetPhase(3);
        }
        else if (turnController.GetPhase() == 3)
        {
            // Check if it is the player's turn
            if (turnController.IsPlayerTurn)
            {
                battleController.DisableAllPhases();
                battleController.SetPhase(2);
            }
            else
            {
                battleController.DisableAllPhases();
                battleController.SetPhase(3);
            }
        }
    }
}
