using UnityEngine;
using UnityEngine.UI;

public class StartBattleButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
   public Button startBattleButton;
   public TurnControllerBehavior turnController;

	void Start () {
		Button btn = startBattleButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		if (turnController.IsPlayerTurn) {
		// start Battle Phase Here
        }
	}
}
