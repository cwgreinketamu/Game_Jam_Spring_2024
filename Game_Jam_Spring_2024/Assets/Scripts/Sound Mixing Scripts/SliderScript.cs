using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderScript : MonoBehaviour
{
    public Camera cam;
    public float minX = -10;
    public float maxX = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, transform.position.y, 0);
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, 0);
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, 0);
        }
    }

}
