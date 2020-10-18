using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
 	private TurnControllerBehavior turnController;
	private ManaBehavior playerMana;

	void Start () {
		turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
		playerMana = GameObject.Find("PlayerMana").GetComponent<ManaBehavior>();

        // Make the button not clickable
        this.GetComponent<Button>().enabled = false;
    }

	public void TaskOnClick()
    {
        // Disable dragging for player before ending the turn if the player is in playing phase
        if (turnController.GetPhase() == 2)
        {
            turnController.DisableDraggingForPlayer();
            turnController.DisableAllNotifications();
        }

        // Make the button not clickable
        this.GetComponent<Button>().enabled = false;

        // End turn routine
        playerMana.IncreaseMana();
        playerMana.ResetMana();
        turnController.DisableAllPhases();
        turnController.SetPhase(1);
        turnController.AlternateTurn();
    }
}
