using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerManaBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private int mana = 1;
    public Slider slider;
    public Image Color;
    public Gradient gradient;
    private static int maxMana = 10;
    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }


    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        // set Mana initially to be 1
        mana = 1;
        SetMana(mana);
        SetMaxMana(mana);
    }
    public void SetMaxMana(int mana)
    {
        slider.value = slider.maxValue = mana;
        Color.color = gradient.Evaluate(10f);
    }
    public void IncreaseMana()
    {
        if (mana < maxMana)
        {
            mana += 1;
        }
    }
    public void ResetMana()
    {
        //mana = maxMana;
        mana = 1;
    }
    void Update()
    {
        // update text of PlayerHealth element
        thisText.text = "Mana: " + mana + "/" + maxMana;
    }
    public void SetMana(int mana)
    {
        slider.value = mana;
        Color.color = gradient.Evaluate(slider.normalizedValue);
    }


    public void DecreaseMana(int amount)
    {
        mana -= amount;//old code
        //if (mana <= amount) mana = amount;//prevents negative numbers
        //SetMana(mana -= amount);//changes health and sets the health, don't touch
    }
}
