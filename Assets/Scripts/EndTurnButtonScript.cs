using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
   public Button endTurnButton;
   public TurnControllerBehavior turnController;

	void Start () {
		Button btn = endTurnButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		if (turnController.IsPlayerTurn) {
            turnController.AlternateTurn();
        }
	}
}
