using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDefendScript : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private Transform playField;
    private List<GameObject> blockCards;
    private Timer timer;
    private bool done;

    public bool Done
    {
        get { return this.done; }
        set { this.done = value; }
    }

    void Awake()
    {
        this.playField = GameObject.Find("OpponentPlayingField").transform;
        this.timer = GameObject.Find("Utility").GetComponent<Timer>();

        blockCards = new List<GameObject>();
    }

    void OnEnable()
    {
        CardBehavior behavior;

        foreach (Transform card in playField)
        {
            behavior = card.GetComponent<CardBehavior>();

            if (!behavior.Blocked)
            {
                blockCards.Add(card.gameObject);
            }
        }

        this.done = false;
        this.timer.SetTimeDelay(timeDelay);
        this.timer.enabled = true;
    }

    void OnDisable()
    {
        blockCards.Clear();

        if (this.timer.enabled)
        {
            this.timer.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.timer.Delayed() && !this.done)
        {
            SelectBlockCard();
        }
    }

    public void SelectBlockCard()
    {
        timer.ResetTimer();

        if (blockCards.Count > 0)
        {
            //Generate a random number between 0 and the highest index inclusively
            System.Random rnd = new System.Random();
            int rndNum = rnd.Next(blockCards.Count);

            CardBehavior behavior = blockCards[rndNum].GetComponent<CardBehavior>();

            behavior.BlockClickRoutine();

            blockCards.Clear();
        }
        else
        {
            this.done = true;
        }
    }
}
