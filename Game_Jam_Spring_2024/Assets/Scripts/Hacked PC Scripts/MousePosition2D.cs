using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition2D : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private CircleCollider2D circleCollider;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false; // Start with collider disabled
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;

        if (Input.GetMouseButtonDown(0))
        {
            circleCollider.enabled = true; // Activate collider when mouse is pressed down
        }
        else if (Input.GetMouseButtonUp(0))
        {
            circleCollider.enabled = false; // Deactivate collider when mouse is released
        }
    }
}
