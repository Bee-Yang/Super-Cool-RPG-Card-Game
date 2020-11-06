using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private int health;
    public Slider slider;
    public Image Color;
    public Gradient gradient;
    private static int maxHealth = 20;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        // set health initially to be 20
        health = maxHealth;
        SetMaxHealth(health);
    }

    void Update()
    {
        // update text of PlayerHealth element
        thisText.text = "Health: " + health + "/" + maxHealth;
    }

    public void SetMaxHealth(int health)
    {
        slider.value = slider.maxValue = health;
        Color.color = gradient.Evaluate(20f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Color.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void DecreaseHealth(int amount)
    {

        if (health <= amount)
        {
            health = 0;//prevents negative numbers
        }
        else
        {
            health -= amount;
        }

        SetHealth(health);//changes health and sets the health, don't touch
    }
}
