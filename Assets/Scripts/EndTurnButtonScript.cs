using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
   	public Button endTurnButton;
 	private TurnControllerBehavior turnController;
	private PlayerManaBehavior manaBehavior;

	void Start () {
		Button btn = endTurnButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
		manaBehavior = GameObject.Find("PlayerMana").GetComponent<PlayerManaBehavior>();
	}

	public void TaskOnClick(){
		if (turnController.IsPlayerTurn) {
			manaBehavior.increaseMana();
            turnController.AlternateTurn();
        }
	}
}
