using UnityEngine;
using TMPro;

public class TextCountBehavior : MonoBehaviour
{
    public GameObject obj;
    private TMP_Text thisText;
    private int count;

    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        
        // set count initially to be 0
        count = 0;
    }

    void Update()
    {
        // When a card goes into graveyard
        if(obj.transform.childCount != count)
        {
             // change the count
             count = obj.transform.childCount;
        }
        
        // update text of PlayerGraveyardCount element
        thisText.text = "" + count;
    }
}
