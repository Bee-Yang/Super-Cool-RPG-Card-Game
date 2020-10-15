using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPhase : MonoBehaviour
{
    private static double timeDelay = 1.0;
    private DeckBehavior player, enemy;
    private TurnControllerBehavior turnController;
    private Timer timer;

    void OnEnable()
    {
        if (!timer)
        {
            timer = GameObject.Find("Utility").GetComponent<Timer>();
        }

        // Enable the timer when this script is enabled
        timer.SetTimeDelay(timeDelay);
        timer.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set values for local variables
        player = GameObject.Find("PlayerDeck").GetComponent<DeckBehavior>();
        enemy = GameObject.Find("OpponentDeck").GetComponent<DeckBehavior>();
        turnController = this.GetComponent<TurnControllerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw a card after the time delay
        if (this.timer.Delayed())
        {
            turnController.CheckGameOverConditions();

            if (turnController.GetPhase() == 1)
            {
                // Draw a card depending on whose turn it is
                if (turnController.IsPlayerTurn)
                {
                    player.Draw();
                }
                else
                {
                    enemy.Draw();
                }

                // Change the phase to playing phase
                turnController.SetPhase(2);
            }

            // Disable the timer
            this.timer.enabled = false;

            // Disable this script
            this.enabled = false;
        }
    }
}
