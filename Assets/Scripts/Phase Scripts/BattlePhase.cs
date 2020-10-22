using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private TurnControllerBehavior turnController;
    private Timer timer;
    private bool start;

    // Temporary variable for opponent turn to function
    private ManaBehavior playerMana, enemyMana;

    public GameObject notification;

    void Awake()
    {
        // Assign the turnController
        turnController = this.GetComponent<TurnControllerBehavior>();

        timer = GameObject.Find("Utility").GetComponent<Timer>();
    }

    void OnEnable()
    {
        start = true;
        timer.SetTimeDelay(timeDelay);
        timer.enabled = true;
    }

    void OnDisable()
    {
        timer.enabled = false;
        turnController.GetComponent<BattleController>().enabled = false;

        GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = false;

        RestoreColorForAttackCards();
        DisableBattleNotification();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMana = GameObject.Find("PlayerMana").GetComponent<ManaBehavior>();
        enemyMana = GameObject.Find("EnemyMana").GetComponent<ManaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the notification finished displaying
        if (this.start)
        {
            BattleNotification();
        }
        // Check if the battle phase is over
        else if (this.GetComponent<BattleController>().GetPhase() == 0)
        {
            if (turnController.IsPlayerTurn)
            {
                playerMana.IncreaseMana();
                playerMana.ResetMana();
            }
            else
            {
                enemyMana.IncreaseMana();
                enemyMana.ResetMana();
            }

            turnController.DisableAllPhases();
            turnController.SetPhase(1);
            turnController.AlternateTurn();
        }
    }

    public void BattleNotification()
    {
        if (this.timer.Delayed())
        {
            // Notify the user about the battle phase by enabling/disabling the notification after a time delay
            notification.transform.SetParent(GameObject.Find("Board").transform);
            notification.SetActive(!notification.activeSelf);

            // Disable this method once user has been notified about the start of the battle phase
            if (!notification.activeSelf)
            {
                notification.transform.SetParent(GameObject.Find("HUD").transform);
                this.start = false;
                this.timer.enabled = false;

                // Enable the battle controller
                this.GetComponent<BattleController>().enabled = true;
            }

            // Disable the timer
            this.timer.ResetTimer();
        }
    }

    public void DisableBattleNotification()
    {
        notification.SetActive(false);
        notification.transform.SetParent(GameObject.Find("HUD").transform);
    }

    public void RestoreColorForAttackCards()
    {
        Transform attackingField;

        if(turnController.IsPlayerTurn)
        {
            attackingField = GameObject.Find("PlayerPlayingField").transform;
        }
        else
        {
            attackingField = GameObject.Find("OpponentPlayingField").transform;
        }

        // Check if the attacking player has creatures in play
        if (attackingField.childCount > 0)
        {
            Transform card;

            // For loop to go through all cards on the playing field
            for (int i = 0; i < attackingField.childCount; i++)
            {
                // Get the card at index i
                card = attackingField.GetChild(i);

                card.GetComponent<Outline>().enabled = false;

                /******* Temporary Code *******************/
                card.GetComponent<CardBehavior>().Attacking = false;
                /******* Temporary Code *******************/

                // Restore the color for the card
                card.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                card.GetChild(1).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
