using UnityEngine;

public class CameraFollowVert : MonoBehaviour
{
    public Transform playerTransform;
    public float verticalOffset = 2f;

    void LateUpdate()
    {
        // Ensure the camera follows the player vertically
        Vector3 newPosition = transform.position;
        newPosition.y = playerTransform.position.y + verticalOffset;
        transform.position = newPosition;
    }
}
