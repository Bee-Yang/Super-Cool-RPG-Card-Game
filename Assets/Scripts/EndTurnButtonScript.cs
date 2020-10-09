using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
   public Button endTurnButton;
 	TurnControllerBehavior turnController;

	void Start () {
		Button btn = endTurnButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
	}

	public void TaskOnClick(){
		if (turnController.IsPlayerTurn) {
            turnController.AlternateTurn();
        }
	}
}
