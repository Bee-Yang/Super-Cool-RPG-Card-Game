using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class EnemyManaBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private int mana;
    public Slider slider;
    public Image color;
    public Gradient gradient;
    private static int maxMana = 1;
    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }


    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        // set Mana initially to be 20
        Mana = maxMana;
        SetMana(Mana);
        SetMaxMana(Mana);
    }
    public void SetMaxMana(int Mana)
    {
        slider.value = slider.maxValue = Mana;
        color.color = gradient.Evaluate(1f);
    }
    public void increaseMana()
    {
        if (mana < maxMana)
        {
            mana += 1;
        }
    }
    public void resetMana()
    {
        mana = maxMana;
    }
    void Update()
    {
        // update text of PlayerHealth element
        thisText.text = "Mana: " + Mana + "/" + maxMana;
    }
    public void SetMana(int mana)
    {
        color.color = gradient.Evaluate(slider.value = mana);
    }


    public void decreaseMana(int amount)
    {

        if (mana <= amount) mana = amount;//prevents negative numbers
        SetMana(mana -= amount);//changes health and sets the health, don't touch
    }
}
