using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayPhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private ManaBehavior mana;
    private Transform AIHand, AIField;
    private Timer timer;
    private bool done;

<<<<<<< HEAD
    public bool Done{
=======
    public bool Done
    {
>>>>>>> origin/bee-branch
        get { return this.done; }
        set { this.done = value; }
    }

    void Awake()
    {
        this.mana = GameObject.Find("EnemyMana").GetComponent<ManaBehavior>();
        this.AIHand = GameObject.Find("OpponentHand").transform;
        this.AIField = GameObject.Find("OpponentPlayingField").transform;
        this.timer = GameObject.Find("Utility").GetComponent<Timer>();
    }

    void OnEnable()
    {
        this.done = false;
        this.timer.SetTimeDelay(timeDelay);
        this.timer.enabled = true;
    }

    void OnDisable()
    {
        if (this.timer.enabled)
        {
            this.timer.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        
=======

>>>>>>> origin/bee-branch
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if(this.timer.Delayed() && !this.done)
=======
        if (this.timer.Delayed() && !this.done)
>>>>>>> origin/bee-branch
        {
            PlayCard();
        }
    }

    public void PlayCard()
    {
        timer.ResetTimer();

        Transform temp, card;
        int tempCost, cardCost;

<<<<<<< HEAD
        if(AIHand.childCount > 0)
=======
        if (AIHand.childCount > 0)
>>>>>>> origin/bee-branch
        {
            card = AIHand.GetChild(0);
            cardCost = card.GetComponent<CardAttributes>().GetCost();

            for (int i = 1; i < AIHand.childCount; i++)
            {
                temp = AIHand.GetChild(i);
                tempCost = temp.GetComponent<CardAttributes>().GetCost();

                if (cardCost > mana.Mana)
                {
                    card = temp;
                    cardCost = tempCost;
                }
                else if (tempCost <= mana.Mana && tempCost > cardCost)
                {
                    card = temp;
                    cardCost = tempCost;
                }
            }

            if (cardCost <= mana.Mana && AIField.childCount < 9)
            {
                mana.DecreaseMana(cardCost);

                card.SetParent(AIField);
                card.GetComponent<CardBehavior>().SetHoverable(true);
                card.GetComponent<CardBehavior>().FlipCard("front");
                card.GetComponent<CardBehavior>().PutInPlay();
            }
            else
            {
                done = true;
            }
        }
        else
        {
            done = true;
        }
    }
}
