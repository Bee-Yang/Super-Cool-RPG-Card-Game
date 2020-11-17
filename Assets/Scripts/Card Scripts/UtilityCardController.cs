using UnityEngine;

public class UtilityCardController : MonoBehaviour
{
    private const int fortify = 100, rally = 101, bloodthirst = 102, murder = 103;
    private GameObject effects;
    private CardAttributes myAttributes;
    private CardBehavior myBehavior;

    private int myID;

    private bool effectUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.effects = GameObject.Find("Effects");
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
                case fortify:
                    this.effects.GetComponent<Fortify>().enabled = true;
                    break;

                case rally:
                    break;

                case bloodthirst:
                    this.effects.GetComponent<Bloodthirst>().enabled = true;
                    break;

                case murder:
                    this.effects.GetComponent<Murder>().enabled = true;
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
