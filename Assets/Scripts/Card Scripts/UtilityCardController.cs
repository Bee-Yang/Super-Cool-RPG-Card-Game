using UnityEngine;

public class UtilityCardController : MonoBehaviour
{
    private const int fortify = 100, rally = 101, bloodthirst = 102, murder = 103;
    private GameObject effects;
    private EffectTargetList targetList;
    private CardAttributes myAttributes;
    private CardBehavior myBehavior;

    private int myID;

    private bool effectUsed = false;
    public bool done = false;

    void OnDisable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        this.effects = GameObject.Find("Effects");
        this.targetList = GameObject.Find("Utility").GetComponent<EffectTargetList>();
        this.myAttributes = this.gameObject.GetComponent<CardAttributes>();
        this.myBehavior = this.gameObject.GetComponent<CardBehavior>();

        this.myID = myAttributes.GetID();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.myBehavior.IsInPlay())
        {
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

                //Set the effect to have been used already
                this.effectUsed = true;
            }
            else if (this.targetList.effectDone)
            {
                this.myBehavior.SetDestroyed();
                this.targetList.enabled = false;
                this.enabled = false;
            }
        }
    }
}
