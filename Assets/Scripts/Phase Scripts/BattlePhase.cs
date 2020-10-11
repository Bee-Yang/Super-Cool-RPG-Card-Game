using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private TurnControllerBehavior turnController;
    private double time;
    private bool start;

    // Temporary variable for opponent turn to function
    private EnemyManaBehavior manaBehavior;

    public GameObject notification;

    void OnEnable()
    {
        start = true;
        time = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        turnController = this.GetComponent<TurnControllerBehavior>();

        // Temporary code for opponent mana to function
        manaBehavior = GameObject.Find("EnemyMana").GetComponent<EnemyManaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        BattleNotification();

        // Temporary code to directly end opponents turn upon entering battle phase
        if (!turnController.IsPlayerTurn && !this.start)
        {
            manaBehavior.increaseMana();
            manaBehavior.resetMana();
            turnController.DisableAllPhases();
            turnController.SetPhase(1);
            turnController.AlternateTurn();
        }
    }

    public void BattleNotification()
    {
        if (this.start)
        {
            // Add time delay to the timer
            this.time += Time.deltaTime;

            if (this.time > timeDelay)
            {
                // Notify the user about the battle phase by enabling/disabling the notification after a time delay
                notification.SetActive(!notification.activeSelf);

                // Disable this method once user has been notified about the start of the battle phase
                if (!notification.activeSelf)
                {
                    this.start = false;
                }

                // Reset the timer
                this.time = 0;
            }
        }
    }
}
