using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private double time;
    private bool start;

    public GameObject notification;

    // Start is called before the first frame update
    void Start()
    {
        start = true;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BattleNotification();
    }

    public void BattleNotification()
    {
        if (this.start)
        {
            // Add time delay to the timer
            this.time += Time.deltaTime;

            if (this.time > timeDelay)
            {
                // Notify the user about the battle phase by enabling/disabling the notification after a time delay
                notification.SetActive(!notification.activeSelf);

                // Disable this method once user has been notified about the start of the battle phase
                if (!notification.activeSelf)
                {
                    this.start = false;
                }

                // Reset the timer
                this.time = 0;
            }
        }
    }
}
