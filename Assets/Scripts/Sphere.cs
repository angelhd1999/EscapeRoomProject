using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public GameObject physicsManager;
    public bool useGravity = true;
    public int makeImpulse = 0;

    private float gravityForce;
    private float impulseForce;
    // Start is called before the first frame update
    void Start()
    {
        gravityForce = physicsManager.GetComponent<PhysicsManager>().gravity;
        impulseForce = physicsManager.GetComponent<PhysicsManager>().impulse;
    }

    public void FixedUpdate()
    {
        if (useGravity)
        {
            GetComponent<Rigidbody>().AddForce(Physics.gravity * gravityForce, ForceMode.Acceleration);
        }
        if (makeImpulse != 0)
        {
            //Add force
            if (makeImpulse == 1)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(1f, 0f, 0f) * impulseForce);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(1f, 0f, 0f) * impulseForce);
            }
            makeImpulse = 0;
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
        if (other.gameObject.CompareTag("LBound"))
        {
            makeImpulse = 1;
        }
        if(other.gameObject.CompareTag("RBound"))
        {
            makeImpulse = 2;
        }
    }
}
