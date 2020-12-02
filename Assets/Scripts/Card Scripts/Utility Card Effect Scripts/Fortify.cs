using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortify : MonoBehaviour
{
    public bool done;

    void OnDisable()
    {
        this.done = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing and disable script
        this.done = true;
    }
}
