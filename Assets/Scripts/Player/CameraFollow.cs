using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the Player Transform in the Inspector
    public Vector3 offset; // Offset to keep some distance (optional)

    private void LateUpdate()
    {
        // Instantly snap the camera to the player's position
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;
    }
}
