using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public Vector3 vector;
    public int velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(vector * Time.deltaTime * velocity);
    }

    public void setVector(Vector3 newvector)
    {
        this.vector = newvector;
    }
}
