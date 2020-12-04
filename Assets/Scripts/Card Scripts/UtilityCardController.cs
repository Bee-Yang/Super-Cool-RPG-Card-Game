using UnityEngine;

public class UtilityCardController : MonoBehaviour
{
    private const int fortify = 100, rally = 101, bloodthirst = 102, murder = 103;
    private GameObject effects;
    private EffectTargetList targetList;
    private CardAttributes myAttributes;
    private CardBehavior myBehavior;

    public int myID;

    public bool effectUsed = false;
    public bool done = false;

    void Awake()
    {
        // Set the variables to their correct value
        this.effects = GameObject.Find("Effects");
        this.targetList = GameObject.Find("Utility").GetComponent<EffectTargetList>();
        this.myAttributes = this.gameObject.GetComponent<CardAttributes>();
        this.myBehavior = this.gameObject.GetComponent<CardBehavior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the ID of the card
        this.myID = myAttributes.GetID();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the card is in play
        if (this.myBehavior.IsInPlay())
        {
            // Check if the effect has been used
            if (!this.effectUsed)
            {
                //Check which utility card is being played
                switch (myID)
                {
                    case fortify:
                        this.effects.GetComponent<Fortify>().enabled = true;
                        break;

                    case rally:
                        this.effects.GetComponent<Rally>().enabled = true;
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

                // Set the effect to have been used already
                this.effectUsed = true;

                // Check if the card is the AI's card and if it can be played
                if (this.tag == "Enemy" && !this.targetList.cannotPlay)
                {
                    // Put the card onto the center of the board
                    this.transform.SetParent(GameObject.Find("Board").transform);
                    this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    this.myBehavior.FlipCard("front");
                    this.myBehavior.SetHoverable(true);
                }

            }
            // Check if the card cannot be played
            else if (this.targetList.cannotPlay)
            {
                // Disable all the effect scripts
                DisableAllEffectScripts();

                // Remove the card from play and set its effectUsed status to false
                this.myBehavior.PutOutOfPlay();
                this.effectUsed = false;

                // Check if the card belongs to the player
                if (this.tag == "Player")
                {
                    // Put the card back into the player's hand
                    this.transform.SetParent(GameObject.Find("PlayerHand").transform);
                    this.myBehavior.SetCurrParent(GameObject.Find("PlayerHand").transform);
                    this.myBehavior.SetDraggable(true);
                }

                // Disable the EffectTargetList script
                this.targetList.enabled = false;
            }
            // Check if the effect is done
            else if (this.targetList.effectDone)
            {
                this.myBehavior.SetDestroyed();     // Destroy the card
                this.targetList.enabled = false;    // Disable the EffectTargetList script
                this.enabled = false;               // Disable this script
            }
        }
    }

    private void DisableAllEffectScripts()
    {
        this.effects.GetComponent<Fortify>().enabled = false;
        this.effects.GetComponent<Rally>().enabled = false;
        this.effects.GetComponent<Bloodthirst>().enabled = false;
        this.effects.GetComponent<Murder>().enabled = false;
    }
}
