using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoving : MonoBehaviour
{
    private float randX;
    private float randY;
    public float speed = 0.25f;
    private int[] modifier = {-1,1}; //used to randomly set the sign of randX and randY
    private GameObject bugSpawner;

    // Start is called before the first frame update
    void Start()
    {
        bugSpawner = GameObject.Find("BugSpawner");
        randX = Random.Range(0.01f, 1.0f) * modifier[Random.Range(0, 2)]; //randX: [-2,-1]U[1,2]
        randY = Random.Range(0.01f, 1.0f) * modifier[Random.Range(0, 2)]; //randY: [-2,-1]U[1,2]
        Vector3 rotate = new Vector3 (randX, randY).normalized;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(rotate.y, rotate.x);
        transform.rotation = Quaternion.Euler(0,0,angle-90);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(randX, randY)*speed;
        if (transform.position.x > 10 || transform.position.x < -10) //wraps around sides of screen
        {
            transform.position = new Vector3(-1*transform.position.x, transform.position.y, 0);
        }
        if (transform.position.y > 6 || transform.position.y < -6) //wraps around bottom/top of screen
        {
            transform.position = new Vector3(transform.position.x, -1 * transform.position.y, 0);
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        bugSpawner.GetComponent<BugSpawning>().KillBug();
    }
}
