using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCollScript : MonoBehaviour
{
    private BoxCollider2D coll;
    public bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inRange = true;
            Debug.Log("in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inRange = false;
            Debug.Log("out range");
        }
    }

    public void SetColl(bool status)
    {
        coll.enabled = status;
    }
}
