using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicCardController : MonoBehaviour
{
    private const int noble = 10, evil = 11;
    private GameObject heroicEffects;
    private CardAttributes myAttributes;
    private CardBehavior myBehavior;

    private int myID;

    private bool effectUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.heroicEffects = GameObject.Find("HeroicEffects");
        this.myAttributes = this.gameObject.GetComponent<CardAttributes>();
        this.myBehavior = this.gameObject.GetComponent<CardBehavior>();

        this.myID = myAttributes.GetID();
    }

    // Update is called once per frame
    void Update()
    {
        if (!effectUsed && myBehavior.IsInPlay())
        {
            //Check which utility card is being played
            switch (myID)
            {
                case noble:
                    this.heroicEffects.GetComponent<Noble>().enabled = true;
                    break;

                case evil:
                    this.heroicEffects.GetComponent<Evil>().enabled = true;
                    break;

                default:
                    break;
            }

            //Set the effect to have been used already
            effectUsed = true;
        }
        else if (this.effectUsed)
        {
            this.enabled = false;
        }
    }
}
