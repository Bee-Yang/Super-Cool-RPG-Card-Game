using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private TurnControllerBehavior turnController;
    private PlayerManaBehavior playerMana;
    private Timer timer;
    private Fader fader;
    private bool start;

    public GameObject playerTurn, enemyTurn, notEnoughManaNotification, outOfMana;

    void OnEnable()
    {
        if (!turnController)
        {
            turnController = this.GetComponent<TurnControllerBehavior>();
        }

        if (!timer)
        {
            timer = GameObject.Find("Utility").GetComponent<Timer>();
        }

        if (!fader)
        {
            fader = GameObject.Find("Utility").GetComponent<Fader>();
        }

        if (turnController.IsPlayerTurn)
        {
            turnController.EnableDraggingForPlayer();
        }

        start = true;

        timer.SetTimeDelay(timeDelay);
        timer.enabled = true;
    }
 
    // Start is called before the first frame update
    void Start()
    {
        playerMana = GameObject.Find("PlayerMana").GetComponent<PlayerManaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnNotification();

        // Temporary code to directly enter battle phase on opponents turn after draw phase
        if (!turnController.IsPlayerTurn && !this.start)
        {
            turnController.DisableAllPhases();
            turnController.SetPhase(3);
        }

        // Check to see if the notification is active
        if (outOfMana.activeSelf)
        {
            // Get the current color of the image
            Color curColor = outOfMana.GetComponent<Image>().color;

            // Check to see if the image has faded out yet
            if (curColor.a < 0.01)
            {
                // Disable the notification and fader
                outOfMana.SetActive(false);
                outOfMana.transform.SetParent(GameObject.Find("HUD").transform);
                this.fader.enabled = false;
            }
        }
    
        // Check to see if the notification is active
        if (notEnoughManaNotification.activeSelf)
        {
            // Get the current color of the image
            Color curColor = notEnoughManaNotification.GetComponent<Image>().color;

            // Check to see if the image has faded out yet
            if (curColor.a < 0.01)
            {
                // Disable the notification and fader
                notEnoughManaNotification.SetActive(false);
                notEnoughManaNotification.transform.SetParent(GameObject.Find("HUD").transform);
                this.fader.enabled = false;
            }
        }
    }

    private void SetStart(bool status)
    {
        this.start = status;
    }

    public void TurnNotification()
    {
        if (this.start)
        {
            if (this.timer.Delayed())
            {
                // Notify the user about whose turn it is by enabling/disabling the turn notification after a time delay
                if (turnController.IsPlayerTurn)
                {
                    if (!playerTurn.activeSelf)
                    {
                        playerTurn.transform.SetParent(GameObject.Find("Board").transform);
                    }
                    else
                    {
                        playerTurn.transform.SetParent(GameObject.Find("HUD").transform);
                    }

                    playerTurn.SetActive(!playerTurn.activeSelf);
                }
                else
                {
                    if (!enemyTurn.activeSelf)
                    {
                        enemyTurn.transform.SetParent(GameObject.Find("Board").transform);
                    }
                    else
                    {
                        enemyTurn.transform.SetParent(GameObject.Find("HUD").transform);
                    }

                    enemyTurn.SetActive(!enemyTurn.activeSelf);
                }

                // Disable this method once user has been notified about whose turn it is
                if (!playerTurn.activeSelf && !enemyTurn.activeSelf)
                {
                    this.start = false;
                    this.timer.enabled = false;

                    if (turnController.IsPlayerTurn)
                    {
                        GameObject.Find("EndTurnButton").GetComponent<Button>().enabled = true;
                    }
                }

                // Reset the timer
                this.timer.ResetTimer();
            }
        }
    }

    public bool CardCanBePlayed(GameObject card)
    {
        // Get the mana cost of the card to be played
        int manaCost = card.GetComponent<CardAttributes>().GetCost();

        // Get the condition for if the card can be played
        bool canBePlayed = manaCost <= playerMana.Mana;

        // Check to see if card can be played
        if (playerMana.Mana <= 0)
        {
            // Enable the out of mana notification
            outOfMana.transform.SetParent(GameObject.Find("Board").transform);
            outOfMana.SetActive(true);
            outOfMana.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            // Enable the fader to fade out the notification
            this.fader.SetImage(outOfMana.GetComponent<Image>());
            this.fader.enabled = true;
        }
        else if (canBePlayed)
        {
            // Decrease the player's mana by the card's cost
            playerMana.DecreaseMana(manaCost);
        }
        else
        {
            // Enable Mana Notification to show that you don't have enough mana to play that card
            notEnoughManaNotification.transform.SetParent(GameObject.Find("Board").transform);
            notEnoughManaNotification.SetActive(true);
            notEnoughManaNotification.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            // Enable the fader to fade out the notification
            this.fader.SetImage(notEnoughManaNotification.GetComponent<Image>());
            this.fader.enabled = true;
        }

        // Return condition of whether the card can be played
        return canBePlayed;
    }

    public void DisableManaNotifications()
    {
        if (notEnoughManaNotification.activeSelf)
        {
            notEnoughManaNotification.SetActive(false);
            notEnoughManaNotification.transform.SetParent(GameObject.Find("HUD").transform);
        }

        if (outOfMana.activeSelf)
        {
            outOfMana.SetActive(false);
            outOfMana.transform.SetParent(GameObject.Find("HUD").transform);
        }

        this.fader.enabled = false;
    }
}
