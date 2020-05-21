using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public GameObject physicsManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity * physicsManager.GetComponent<PhysicsManager>().gravity, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
