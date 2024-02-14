using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDestroy : MonoBehaviour
{
    private PopUpSpawner spawner;

    void Start()
    {
        // Find the PopUpSpawner in the scene
        spawner = FindObjectOfType<PopUpSpawner>();
    }

    private void OnMouseDown()
    {
        // Call the method to decrease the remaining windows count in PopUpSpawner
        spawner.DecreaseWindowsCount();
        Destroy(gameObject);
    }
}
