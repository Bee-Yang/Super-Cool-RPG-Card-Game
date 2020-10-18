using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private int mana, maxMana;
    public Slider slider;
    public Image Color;
    public Gradient gradient;
    private static int manaCap = 10;

    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }


    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        // set Mana initially to be 1
        maxMana = 1;
        mana = maxMana;
        SetMaxMana(maxMana);
    }

    void Update()
    {
        // update text of PlayerHealth element
        thisText.text = "Mana: " + mana + "/" + maxMana;
    }

    public void SetMaxMana(int mana)
    {
        slider.value = slider.maxValue = mana;
        Color.color = gradient.Evaluate(10f);
    }

    public void IncreaseMana()
    {
        if (maxMana < manaCap)
        {
            maxMana += 1;
        }
    }

    public void ResetMana()
    {
        mana = maxMana;

        SetMaxMana(maxMana);
    }

    public void SetMana(int mana)
    {
        slider.value = mana;
        Color.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void DecreaseMana(int amount)
    {
        mana -= amount;//old code
        SetMana(mana);
        //if (mana <= amount) mana = amount;//prevents negative numbers
        //SetMana(mana -= amount);//changes health and sets the health, don't touch
    }
}
