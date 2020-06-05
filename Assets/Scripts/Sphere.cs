using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public GameObject physicsManager;
    public bool useGravity = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        if (useGravity)
        {
            GetComponent<Rigidbody>().AddForce(Physics.gravity * physicsManager.GetComponent<PhysicsManager>().gravity, ForceMode.Acceleration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 200;
        transform.position = pos; //To block their z movement.
    }
}
