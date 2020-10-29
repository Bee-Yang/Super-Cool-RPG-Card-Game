using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlipButtons : MonoBehaviour
{
    public TurnControllerBehavior turnController;
    public TMP_Text coinFlipText;
    public GameObject coinFlipPrompt;
    public Button headsButton;
    public Button tailsButton;
    private int coinSide;
    private int playerChoice = -1;

    // Start is called before the first frame update
    void Start()
    {
        turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        coinFlipText = GameObject.Find("CoinFlipText").GetComponent<TMP_Text>();
        coinFlipPrompt = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tails()
    {
        disableButtons();
        coinFlipText.SetText("You have picked Tails");
        playerChoice = 1;
        StartCoroutine(flipCoin());
    }

    public void Heads()
    {
        disableButtons();
        coinFlipText.SetText("You have picked Heads");
        playerChoice = 0;
        StartCoroutine(flipCoin());
    }

    IEnumerator flipCoin()
    {
        System.Random rnd = new System.Random();
        coinSide = rnd.Next(0, 2);

        yield return new WaitForSecondsRealtime(2);

        if (coinSide == playerChoice)
        {
            coinFlipText.SetText("You picked correctly!\nYou go first.");
            yield return new WaitForSecondsRealtime(2);
            turnController.IsPlayerTurn = true;
        }

        else
        {
            coinFlipText.SetText("You picked incorrectly!\nYou go second.");
            yield return new WaitForSecondsRealtime(2);
            turnController.IsPlayerTurn = false;
        }

        turnController.phase = 0;
        Destroy(coinFlipPrompt);
    }

    void disableButtons()
    {
        tailsButton.enabled = false;
        headsButton.enabled = false;
    }
}
