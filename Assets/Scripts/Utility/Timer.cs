using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private double timeDelay;
    private double timer;

    void OnEnable()
    {
        // Reset timer when this script is enabled
        this.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Increment timer
        this.timer += Time.deltaTime;
    }

    public double GetTimeDelay()
    {
        // Return the value of time delay
        return this.timeDelay;
    }

    public double GetTimer()
    {
        // Return the value of the timer
        return this.timer;
    }

    public bool Delayed()
    {
        // Return whether the time delay has been reached
        return this.timer > this.timeDelay;
    }

    public void SetTimeDelay(double newDelay)
    {
        // Set the value of the time delay
        this.timeDelay = newDelay;
    }

    public void ResetTimer()
    {
        // Reset the value of the timer
        this.timer = 0;
    }
}
