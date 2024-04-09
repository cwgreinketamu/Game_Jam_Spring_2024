using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    private CircleCollider2D coll;
    private SpriteRenderer sprite;
    private DotSpawnerScript dotSpawnerScript;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        dotSpawnerScript = GameObject.Find("DotSpawner").GetComponent<DotSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableCollider()
    {
        coll.enabled = false;
        sprite.color = Color.blue;
    }

    private void OnMouseOver()
    {
        bool clicked = dotSpawnerScript.DotDrawn();
        if (clicked)
        {
            DisableCollider();
        }
    }
}
