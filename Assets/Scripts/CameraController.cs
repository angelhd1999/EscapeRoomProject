using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
