using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
//find in UnityCodeMonkey.com

public class HealthMeterHandlerCode : MonoBehaviour
{

    [SerializedField] private HealthMeter healthmeter;
    // Start is called before the first frame update
    private void Start()
    {
        float health = 1f;
        //1f represents a full health meter, full red
        //use decimal float to change to eq. .5f which is 50% full
        //scale is 0: empty and 1: full
        FunctionPeriodic.Create(() =>
        {
            if (health > 0)
            {
                health = 1f;//health can change here
                healthmeter.SetSize(health);
                //animation
                //if under 30% health flash meter white and red
                if ((health * 100f) % 3 == 0)
                {
                    healthmeter.SetColor(Color.white);
                }  else
                {
                    healthmeter.SetColor(Color.red);
                }
            }
        }, .09f);//Every 90 milliseconds update this functionPeriodic
    }
    // healthmeter.SetColor(Color.blue);
    // Update is called once per frame
    void Update()
    {
        
    }
}
