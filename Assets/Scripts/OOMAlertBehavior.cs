using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OOMAlertBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private PlayerManaBehavior playerManaBehavior;
    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        playerManaBehavior = GameObject.FindWithTag("PlayerMana").GetComponent<PlayerManaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManaBehavior.Mana == 0){
            thisText.text = "You are out of mana!";
        }
    }
}
