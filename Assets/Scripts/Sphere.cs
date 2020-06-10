using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public GameObject physicsManager;
    public bool useGravity = true;
    public int makeImpulse = 0;

    private float gravityForce;
    // Start is called before the first frame update
    void Start()
    {
        gravityForce = physicsManager.GetComponent<PhysicsManager>().gravity;
    }

    public void FixedUpdate()
    {
        if (useGravity)
        {
            GetComponent<Rigidbody>().AddForce(Physics.gravity * gravityForce, ForceMode.Acceleration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 200;
        transform.position = pos; //To block their z movement.
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bounds"))
        {
            transform.position = new Vector3(400, -25, 200);
        }
    }
}
