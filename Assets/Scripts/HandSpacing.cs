using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSpacing : MonoBehaviour
{
    public float spacing;

    // Start is called before the first frame update
    void Start()
    {
        this.spacing = this.GetComponent<HorizontalLayoutGroup>().spacing;
    }

    // Update is called once per frame
    void Update()
    {
        SetHandSpacing();
    }

    public void SetHandSpacing()
    {
        // Set spacing for hand if there are more than 9 cards in the hand
        if (this.transform.childCount > 9)
        {
            float newSpacing;
            
            newSpacing = (1228 / this.transform.childCount) - 137;

            if (this.spacing != newSpacing)
            {
                this.spacing = newSpacing;

                this.GetComponent<HorizontalLayoutGroup>().spacing = spacing;
            }
        }
        else
        {
            this.GetComponent<HorizontalLayoutGroup>().spacing = 5;
        }
    }
}
