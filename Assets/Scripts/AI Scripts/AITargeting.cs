using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargeting : MonoBehaviour
{
    private EffectTargetList targetList;
    private bool selecting;

    void Awake()
    {
        targetList = GameObject.Find("Utility").GetComponent<EffectTargetList>();
    }

    void OnEnable()
    {
        this.selecting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.selecting)
        {
            this.selecting = true;

            SelectTarget();
        }
    }

    private void SelectTarget()
    {
        // Generate a random number between 0 and the highest index inclusively
        System.Random rnd = new System.Random();
        int rndNum = rnd.Next(0, targetList.targets.Count);

        // Select the random card as the target
        GameObject tmp = targetList.targets[rndNum];

        targetList.targets.Clear();
        targetList.targets.Add(tmp);

        targetList.targetDone = true;

        this.enabled = false;
    }
}
