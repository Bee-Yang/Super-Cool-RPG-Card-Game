﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIAttackScript : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private Transform playField;
    private List<GameObject> attackCards;
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

        attackCards = new List<GameObject>();
    }

    void OnEnable()
    {
        foreach (Transform card in playField)
        {
            attackCards.Add(card.gameObject);
        }

        this.done = false;
        this.timer.SetTimeDelay(timeDelay);
        this.timer.enabled = true;
    }

    void OnDisable()
    {
        attackCards.Clear();

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
            SelectAttackCard();
        }
    }

    public void SelectAttackCard()
    {
        timer.ResetTimer();

        if (attackCards.Count > 0)
        {
            CardBehavior behavior = attackCards[0].GetComponent<CardBehavior>();

            // Make card attack
            behavior.AttackClickRoutine();

            attackCards.Remove(attackCards[0]);
        }
        else
        {
            this.done = true;
        }
    }
}
