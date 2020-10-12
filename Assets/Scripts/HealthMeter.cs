using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Transform sprite;
    // Start is called before the first frame update
    private void Start()
    {
        sprite = transform.Find("Sprite");
        
    }
    public void SetSize(float sizeNormalized)
    {
        sprite.localScale = new Vector3(sizeNormalized, 1f);
    }
    public void SetColor(Color color)
    {
        sprite.Find("Sprite").GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
