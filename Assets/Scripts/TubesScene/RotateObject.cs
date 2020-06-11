using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public List<GameObject> Children;
    public Vector3 vector;
    public int velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject child in Children)
        {
            //child is your child transform
            child.transform.Rotate(vector * Time.deltaTime * velocity);
        }
     
    }

    public void setVector(Vector3 newvector)
    {
        this.vector = newvector;
    }
}
