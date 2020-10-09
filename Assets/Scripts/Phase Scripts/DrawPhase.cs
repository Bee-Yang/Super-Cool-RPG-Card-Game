using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPhase : MonoBehaviour
{
    private static double timeDelay = 1.0;
    private DeckBehavior player, enemy;
    private TurnControllerBehavior turnController;
    private double time;

    // Start is called before the first frame update
    void Start()
    {
        // Set values for local variables
        player = GameObject.Find("PlayerDeck").GetComponent<DeckBehavior>();
        enemy = GameObject.Find("OpponentDeck").GetComponent<DeckBehavior>();
        turnController = this.GetComponent<TurnControllerBehavior>();

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current phase is drawing phase based on turnController
        if (turnController.GetPhase() == 1)
        {
            // Add time delay to the timer before drawing a card
            time += Time.deltaTime;

            // Draw a card after the time delay
            if (time > timeDelay)
            {
                // Draw a card depending on whose turn it is
                if (turnController.GetIsPlayerTurn())
                {
                    player.Draw();
                }
                else
                {
                    enemy.Draw();
                }

                // Reset timer
                time = 0;

                // Change the phase to playing phase
                turnController.SetPhase(2);

                // Disable this script
                this.enabled = false;
            }
        }
    }
}
