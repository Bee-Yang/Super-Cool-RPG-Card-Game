﻿using UnityEngine;
using UnityEngine.UI;

public class PlayingPhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private static int maxCreaturesPlayed = 9;
    private TurnControllerBehavior turnController;
    private GameObject AI;
    private ManaBehavior playerMana;
    private Timer timer;
    private Fader fader;
    private bool start, firstTurn;

    public GameObject playerTurn, enemyTurn, notEnoughManaNotification, outOfMana;

    public bool FirstTurn
    {
        get { return this.firstTurn; }
        set { this.firstTurn = value; }
    }

    void Awake()
    {
        // Assign the turnController
        turnController = this.GetComponent<TurnControllerBehavior>();

        // Assign the playerMana
        playerMana = GameObject.Find("PlayerMana").GetComponent<ManaBehavior>();

        AI = GameObject.Find("AI");

        timer = GameObject.Find("Utility").GetComponent<Timer>();

        fader = GameObject.Find("Utility").GetComponent<Fader>();
    }

    void OnEnable()
    {
        start = true;

        timer.SetTimeDelay(timeDelay);
        timer.enabled = true;
    }

    void OnDisable()
    {
        // Check if it is the player's playing phase
        if (turnController.IsPlayerTurn)
        {
            // Disable dragging for the player
            turnController.DisableDraggingForPlayer();
        }

        if (timer.enabled)
        {
            timer.enabled = false;
        }

        GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = false;

        DisableTurnNotifications();
        DisableManaNotifications();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it is the start of the playing phase
        if (this.start)
        {
            TurnNotification();
        }
        else
        {
            // Check if it is the AI's turn
            if (!turnController.IsPlayerTurn)
            {
                // Do the AI play phase routine
                AIPlayPhaseRoutine();
            }
            else
            {
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
        }
    }

    private void SetStart(bool status)
    {
        this.start = status;
    }

    public void TurnNotification()
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

                // Check if it is the player's turn
                if (turnController.IsPlayerTurn)
                {
                    // Enable dragging for the player
                    turnController.EnableDraggingForPlayer();

                    // Enable the start battle and end turn buttons
                    GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = true;

                    // Check if it is the first turn of the duel
                    if (!this.firstTurn)
                    {
                        GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        this.firstTurn = false;
                    }
                }
            }

            // Reset the timer
            this.timer.ResetTimer();
        }
    }

    public bool CardCanBePlayed(GameObject card)
    {
        // Get the target playing field of the card
        Transform field;

        if (turnController.IsPlayerTurn)
        {
            field = GameObject.Find("PlayerPlayingField").transform;
        }
        else
        {
            field = GameObject.Find("OpponentPlayingField").transform;
        }

        // Get the mana cost of the card to be played
        int manaCost = card.GetComponent<CardAttributes>().GetCost();

        // Get the condition for if the card can be played
        bool canBePlayed = (manaCost <= playerMana.Mana) && (field.childCount < 9);

        // Check to see if card can be played
        if (canBePlayed)
        {
            // Decrease the player's mana by the card's cost
            playerMana.DecreaseMana(manaCost);
        }
        else if (field.childCount == maxCreaturesPlayed)
        {
            // Debug statement to notify for max amount of creatures
            Debug.Log("Cannot play any more creatures. Max amount of creatures played");
        }
        else if (playerMana.Mana <= 0)
        {
            // Enable the out of mana notification
            outOfMana.transform.SetParent(GameObject.Find("Board").transform);
            outOfMana.SetActive(true);
            outOfMana.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            // Enable the fader to fade out the notification
            this.fader.SetImage(outOfMana.GetComponent<Image>());
            this.fader.enabled = true;
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

    public void DisableTurnNotifications()
    {
        playerTurn.transform.SetParent(GameObject.Find("HUD").transform);
        playerTurn.SetActive(false);
        enemyTurn.transform.SetParent(GameObject.Find("HUD").transform);
        enemyTurn.SetActive(false);
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

    public void AIPlayPhaseRoutine()
    {
        // Check if the AI playing phase script is enabled
        if (!AI.GetComponent<AIPlayPhase>().enabled)
        {
            // Enable the AI playing phase script
            AI.GetComponent<AIPlayPhase>().enabled = true;
        }
        // Check if the AI is done with the playing phase
        else if (AI.GetComponent<AIPlayPhase>().Done)
        {
            // Disable the AI playing phase script and go into the battle phase
            AI.GetComponent<AIPlayPhase>().enabled = false;

            if (this.firstTurn)
            {
                ManaBehavior mana = GameObject.Find("EnemyMana").GetComponent<ManaBehavior>();

                mana.IncreaseMana();
                mana.ResetMana();

                turnController.SetPhase(1);
                turnController.AlternateTurn();

                this.firstTurn = false;
            }
            else
            {
                turnController.SetPhase(3);
            }

            this.enabled = false;
        }
    }
}