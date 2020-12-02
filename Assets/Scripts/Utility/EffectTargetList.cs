using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTargetList : MonoBehaviour
{
    public List<GameObject> targets;
    public bool targetDone, effectDone;

    void Awake()
    {
        this.targets = new List<GameObject>();
        this.targetDone = false;
        this.effectDone = false;
    }

    void OnDisable()
    {
        this.targets.Clear();
        this.targetDone = false;
        this.effectDone = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
