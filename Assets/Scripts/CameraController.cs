using UnityEngine;

/// <summary>
/// Simple script that makes the camera follows the position and rotation of the head of the player.
/// </summary>
public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player = null;
    [SerializeField] private Vector3 offset = new Vector3();

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (player == null)
        {
            Debug.LogWarning("Missing player ref !", this);

            return;
        }

        //Compute position
        transform.position = player.TransformPoint(offset);
        //Compute rotation
        transform.LookAt(player);
    }
}
