using UnityEngine;

public class TurnControllerBehavior : MonoBehaviour
{
    private bool isPlayerTurn;
    private bool gameOver;
    public const int beginning = 0, drawPhase = 1, playPhase = 2, battlePhase = 3;
    public const int win = -1, lose = -2;

    public int phase;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject board;
    public GameObject opponentDeck;
    public GameObject playerDeck;
    public GameObject playerHealth;
    public GameObject enemyHealth;


    public bool IsPlayerTurn {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }

    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    void Start()
    {
        isPlayerTurn = true;
        gameOver = false;
        phase = 0;

        DisableAllPhases();
    }

    void Update()
    {
        if (!gameOver)
        {
            // Player won
            if (phase == win)
            {
                gameOver = true;
                GameObject tmp = (Instantiate(winScreen, board.transform) as GameObject);
            }

            // Player lost
            if (phase == lose)
            {
                gameOver = true;
                GameObject tmp = (Instantiate(loseScreen, board.transform) as GameObject);
            }
        }

        if (isPlayerTurn) {
            playerGo();
        } 
        else {
            opponentGo();
        }
    }
    public void AlternateTurn() {
        isPlayerTurn = !isPlayerTurn;
    }

    void playerGo()
    {
        //add calls for functions for draw card, and begin battle here. turn ended manually by button
        switch(phase)
        {
            // Enable the script for drawing 5 cards at the beginning of the duel
            case beginning:
                this.GetComponent<Beginning>().enabled = true;
                break;

            // Enable the script for drawing phase
            case drawPhase:
                this.GetComponent<DrawPhase>().enabled = true;
                break;
               
            // Enable the script for playing phase
            case playPhase:
                this.GetComponent<PlayingPhase>().enabled = true;
                break;
            
            // Enable the script for battle phase
            case battlePhase:
                this.GetComponent<BattlePhase>().enabled = true;
                break;

            default:
                DisableAllPhases();
                break;
        }
    }
    void opponentGo()
    {
        //phases will be controlled here, turn ended automatically
        switch (phase)
        {
            // Enable the script for drawing 5 cards at the beginning of the duel
            case beginning:
                this.GetComponent<Beginning>().enabled = true;
                break;

            // Enable the script for drawing phase
            case drawPhase:
                this.GetComponent<DrawPhase>().enabled = true;
                break;

            // Enable the script for playing phase
            case playPhase:
                this.GetComponent<PlayingPhase>().enabled = true;
                break;

            // Enable the script for battle phase
            case battlePhase:
                this.GetComponent<BattlePhase>().enabled = true;
                break;

            default:
                DisableAllPhases();
                break;
        }
    }

    public int GetPhase()
    {
        return this.phase;
    }

    public void SetPhase(int nextPhase)
    {
        this.phase = nextPhase;
    }

    public void DisableAllPhases()
    {
        this.GetComponent<Beginning>().enabled = false;
        this.GetComponent<DrawPhase>().enabled = false;
        this.GetComponent<PlayingPhase>().enabled = false;
        this.GetComponent<BattlePhase>().enabled = false;
    }

    // Enable dragging for player
    public void EnableDraggingForPlayer()
    {
        Transform hand = GameObject.Find("PlayerHand").transform;

        for (int i = 0; i < hand.childCount; ++i)
        {
            hand.GetChild(i).GetComponent<CardBehavior>().SetDraggable(true);
        }
    }

    // Disable dragging for player
    public void DisableDraggingForPlayer()
    {
        Transform hand = GameObject.Find("PlayerHand").transform;

        for (int i = 0; i < hand.childCount; ++i)
        {
            hand.GetChild(i).GetComponent<CardBehavior>().SetDraggable(false);
        }
    }

    public void DisableAllNotifications()
    {
        this.GetComponent<PlayingPhase>().DisableManaNotifications();
    }

    public void CheckGameOverConditions()
    {
        bool playerWin, playerLose;

        // Set if the player has won based on the win conditions (enemy ran out of cards or enemy health is 0)
        playerWin = (!isPlayerTurn && opponentDeck.transform.childCount == 0) || (enemyHealth.GetComponent<HealthBehavior>().Health == 0);

        // Set if player has lost based on the lose conditions (player ran out of cards or player health is 0)
        playerLose = (isPlayerTurn && playerDeck.transform.childCount == 0) || (playerHealth.GetComponent<HealthBehavior>().Health == 0);

        // Check if the player has won
        if (playerWin)
        {
            phase = -1;
        }

        // Check if the player has lost
        if (playerLose)
        {
            phase = -2;
        }
    }
}