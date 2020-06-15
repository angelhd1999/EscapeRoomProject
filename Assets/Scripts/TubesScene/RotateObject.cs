using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to rotate a crank.
/// </summary>
public class RotateObject : MonoBehaviour
{
    public List<GameObject> Children;
    public Vector3 vector;
    public int velocity;

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject child in Children)
        {
            //It rotates each child component.
            child.transform.Rotate(vector * Time.deltaTime * velocity);
        }

    }

    //To change rotation vector.
    public void setVector(Vector3 newvector)
    {
        this.vector = newvector;
    }
}
