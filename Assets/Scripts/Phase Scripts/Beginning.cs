using UnityEngine;


public class Beginning : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private static int beginningHand = 5;
    private DeckBehavior player, enemy;
    private TurnControllerBehavior turnController;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        // Set the value of local variables
        player = GameObject.Find("PlayerDeck").GetComponent<DeckBehavior>();
        enemy = GameObject.Find("OpponentDeck").GetComponent<DeckBehavior>();
        turnController = this.GetComponent<TurnControllerBehavior>();

        timer = GameObject.Find("Utility").GetComponent<Timer>();
        timer.SetTimeDelay(timeDelay);
        timer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Draw a card after time delay
        if (this.timer.Delayed())
        {
            Transform hand = GameObject.Find("PlayerHand").transform;

            // Draw a card for each player if the current hand is less than the specified amount
            if (hand.childCount < beginningHand)
            {
                player.Draw();
                enemy.Draw();

                // Reset timer
                this.timer.ResetTimer();

                // Check if each player has the amount of cards specified for the beginning fo the duel
                if (hand.childCount == beginningHand)
                {
                    // Change the phase to drawing phase
                    turnController.SetPhase(1);

                    // Disable this script
                    this.timer.enabled = false;
                    this.enabled = false;
                }
            }
        }
    }
}
