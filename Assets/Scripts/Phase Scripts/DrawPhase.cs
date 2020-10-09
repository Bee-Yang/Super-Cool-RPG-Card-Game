using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // Add time delay to the timer before drawing a card
        time += Time.deltaTime;

        // Draw a card after the time delay
        if (time > timeDelay)
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

            // Reset timer
            time = 0;

            // Change the phase to playing phase
            turnController.SetPhase(2);

            // Disable this script
            this.enabled = false;
        }
    }
}
