using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Beginning : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private static int beginningHand = 5;
    private DeckBehavior player, enemy;
    private TurnControllerBehavior turnController;
    private double time;

    // Start is called before the first frame update
    void Start()
    {
        // Set the value of local variables
        player = GameObject.Find("PlayerDeck").GetComponent<DeckBehavior>();
        enemy = GameObject.Find("OpponentDeck").GetComponent<DeckBehavior>();
        turnController = this.GetComponent<TurnControllerBehavior>();

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Add time delay to the timer between drawing a card
        time += Time.deltaTime;

        // Draw a card after time delay
        if (time > timeDelay)
        {
            Transform hand = GameObject.Find("Hand-Player").transform;

            // Draw a card for each player if the current hand is less than the specified amount
            if (hand.childCount < beginningHand)
            {
                player.Draw();
                enemy.Draw();

                // Reset timer
                time = 0;

                // Check if each player has the amount of cards specified for the beginning fo the duel
                if (hand.childCount == beginningHand)
                {
                    // Change the phase to drawing phase
                    turnController.SetPhase(1);

                    // Disable this script
                    this.enabled = false;
                }
            }
        }
    }
}
