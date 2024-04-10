using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    public bool status; //true = on, false = off
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    private GameObject switchController;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        switchController = GameObject.Find("SwitchController");
        int roll = Random.Range(0, 10);
        status = roll >= 7;
        SetStatus(status);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        SetStatus(true);
    }

    public void SetStatus(bool status)
    {
        if (status) //turn on
        {
            spriteRenderer.sprite = onSprite;
            coll.enabled = false;
            switchController.GetComponent<SwitchControllerScript>().IncreaseCounter();
        }
        else //turn off
        {
            spriteRenderer.sprite = offSprite;
            coll.enabled = true;
        }
    }
}
