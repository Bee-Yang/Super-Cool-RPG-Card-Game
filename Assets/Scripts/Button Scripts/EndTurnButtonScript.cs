using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
 	private TurnControllerBehavior turnController;
	private ManaBehavior playerMana;

    private void Awake()
    {
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        playerMana = GameObject.Find("PlayerMana").GetComponent<ManaBehavior>();

        // Make the button not clickable
        this.GetComponent<Button>().interactable = false;
    }

    void Start ()
    {
    }

	public void TaskOnClick()
    {
        // Make the buttons not clickable
        this.GetComponent<Button>().interactable = false;
        GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = false;

        // End turn routine
        playerMana.IncreaseMana();
        playerMana.ResetMana();
        turnController.DisableAllPhases();
        turnController.SetPhase(1);
        turnController.AlternateTurn();
    }
}
