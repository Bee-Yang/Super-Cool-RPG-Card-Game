using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppoDraw : MonoBehaviour
{
    public GameObject Card;
    public GameObject CardStack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CardStack = GameObject.Find("Opponent-Player");
        Card.transform.SetParent(Opponent-Player.transform);
        Card.transform.localScale = Vector3.one;
        Card.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
        Card.transform.eulerAngles = new Vector3(25, 0, 0);

    }
}
