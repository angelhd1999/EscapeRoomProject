using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeManager : MonoBehaviour
{
    [SerializeField] private GameObject pose;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selectionR;
    private Transform _selectionL;
    private string objectNameR;
    private string objectNameL;
    [SerializeField] private bool couroutineRunnigR = false;
    private bool couroutineRunnigL = false;
    private bool couroutineMovingR = false;
    private bool couroutineMovingL = false;
    private bool setRight = true;
    private bool setLeft = false;
    Vector3 hitPointR;
    Vector3 hitPointL;

    private GameObject selectedCrankRight = null;
    private GameObject selectedCrankLeft = null;

    public GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_selectionR != null)
        {
            //DeHighlightSphere(_selectionR, objectNameR);
        }
        if (_selectionL != null)
        {
            //DeHighlightSphere(_selectionL, objectNameL);

        }
        //Right Wrist
        RaycastHit hitR;
        // The ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position), out hitR, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position) * 1000, Color.yellow);
            var selection = hitR.transform;
            if (selection.CompareTag(selectableTag))
            {
                //Debug.Log(hitR.collider.gameObject.name);
                _selectionR = selection;
                objectNameR = hitR.collider.gameObject.name;

                //Assing selected crank.
                Debug.Log(objectNameR);
                selectedCrankRight = hitR.collider.gameObject;
                //RotateCrank.
                rotateCrank(selectedCrankRight, new Vector3(0, 0, -1), "right");

                /*Rotate with vector (0, 0, -1) and velocity 50*/
                /*Increase RGB value of color selected (max 1)*/
                //HighlightSphere(_selectionR, objectNameR);
                //CheckPick(setRight);
            }
            else
            {
                if(selectedCrankRight != null)
                {
                    selectedCrankRight.GetComponent<RotateObject>().enabled = false;
                    selectedCrankRight = null;
                }
                objectNameR = hitR.collider.gameObject.name; //The object is not selectable.
                hitPointR = hitR.point; //Point where object hit.
            }
        }
        else
        {
            objectNameR = "";
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
        //Left Wrist
        RaycastHit hitL;
        // The ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristL.transform.position - Camera.main.transform.position), out hitL, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristL.transform.position - Camera.main.transform.position) * 1000, Color.yellow);
            var selection = hitL.transform;
            if (selection.CompareTag(selectableTag))
            {
                //Debug.Log(hitL.collider.gameObject.name);
                _selectionL = selection;
                objectNameL = hitL.collider.gameObject.name;

                //Assing selected crank.
                selectedCrankLeft = hitL.collider.gameObject;
                //RotateCrank.
                rotateCrank(selectedCrankLeft, new Vector3(0, 0, 1), "left");

                /*Rotate with vector (0, 0, 1) and velocity 50*/
                /*Decrease RGB value of color selected (min 0)*/
                //HighlightSphere(_selectionL, objectNameL);
                //CheckPick(setLeft);
            }
            else
            {
                objectNameL = hitL.collider.gameObject.name; //The object is not selectable.
                hitPointL = hitL.point; //Point where object hit.
            }
        }
        else
        {
            if (selectedCrankLeft != null)
            {
                selectedCrankLeft.GetComponent<RotateObject>().enabled = false;
                selectedCrankLeft = null;
            }
            objectNameL = "";
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristL.transform.position - Camera.main.transform.position) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }

    void rotateCrank(GameObject crank, Vector3 rotationVector, string hand)
    {
        crank.GetComponent<RotateObject>().setVector(rotationVector);
        //What crank was selected.
        switch (crank.name)
        {
            case "ManibelaVermella":
                crank.GetComponent<RotateObject>().enabled = true;
                if (String.Equals(hand, "right")) smoke.GetComponent<SmokeScript>().increaseColor(0.01f, 0.0f, 0.0f);
                else smoke.GetComponent<SmokeScript>().increaseColor(-0.01f, 0.0f, 0.0f);
                break;
            case "ManibelaVerda":
                crank.GetComponent<RotateObject>().enabled = true;
                if(String.Equals(hand, "right")) smoke.GetComponent<SmokeScript>().increaseColor(0.0f, 0.01f, 0.0f);
                else smoke.GetComponent<SmokeScript>().increaseColor(0.0f, -0.01f, 0.0f);
                break;
            case "ManibelaBlava":
                crank.GetComponent<RotateObject>().enabled = true;
                if (String.Equals(hand, "right")) smoke.GetComponent<SmokeScript>().increaseColor(0.0f, 0.0f, 0.01f);
                else smoke.GetComponent<SmokeScript>().increaseColor(0.0f, 0.0f, -0.01f);
                break;
            default:
                break;
        }
    }

    //With absolute value
    public bool Approximately(Vector3 me, Vector3 other, float allowedDifference)
    {
        var dx = me.x - other.x;
        if (Mathf.Abs(dx) > allowedDifference)
            return false;

        var dy = me.y - other.y;
        if (Mathf.Abs(dy) > allowedDifference)
            return false;

        var dz = me.z - other.z;

        return Mathf.Abs(dz) >= allowedDifference;
    }
    /*
   void CheckPick(bool right)
   {
       if (right)
       {
           if (!couroutineRunnigR)
           {
               couroutineRunnigR = true;
               StartCoroutine("PickSphereR");
           }
       }
       else
       {
           if (!couroutineRunnigL)
           {
               couroutineRunnigL = true;
               StartCoroutine("PickSphereL");
           }
       }
   }

   IEnumerator PickSphereR()
   {
       string firstObjectName = objectNameR;
       Transform firstObjectSelected = _selectionR;
       //Debug.Log(firstObjectName);
       float startTime = Time.time;
       while (couroutineRunnigR)
       {
           if (firstObjectName != objectNameR)
           {
               couroutineRunnigR = false;
               yield break;
           }
           if (Time.time - startTime > 1.5f)
           {
               Debug.Log("Achieved" + firstObjectName);
               CheckMove(firstObjectSelected, setRight);
               yield break;
           }
           yield return new WaitForSeconds(0.1f);
       }
       yield break;
   }

   IEnumerator PickSphereL()
   {
       string firstObjectName = objectNameL;
       Transform firstObjectSelected = _selectionL;
       //Debug.Log(firstObjectName);
       float startTime = Time.time;
       while (couroutineRunnigL)
       {
           if (firstObjectName != objectNameL)
           {
               couroutineRunnigL = false;
               yield break;
           }
           if (Time.time - startTime > 1.5f)
           {
               Debug.Log("Achieved" + firstObjectName);
               CheckMove(firstObjectSelected, setLeft);
               yield break;
           }
           yield return new WaitForSeconds(0.1f);
       }
       yield break;
   }
   void CheckMove(Transform selection, bool hand)
   {
       if (hand)
       {
           if (!couroutineMovingR)
           {
               couroutineMovingR = true;
               StartCoroutine(MoveSphere(selection, hand));
           }
       }
       else
       {
           if (!couroutineMovingL)
           {
               couroutineMovingL = true;
               StartCoroutine(MoveSphere(selection, hand));
           }
       }
   }

   IEnumerator MoveSphere(Transform selection, bool right)
   {
       float startTime = Time.time;
       bool settedPosition = false;
       Vector3 prev_position = selection.position;
       if (right)
       {
           while (couroutineMovingR)
           {
               Debug.Log("inMoveSphere loop");
               if (!settedPosition)
               {
                   prev_position = selection.position;
                   settedPosition = true;
               }
               selection.gameObject.layer = 2;
               selection.gameObject.GetComponent<Sphere>().useGravity = false;
               selection.position = hitPointR;
               if (Vector3.Distance(prev_position, selection.position) > 50)
               {
                   startTime = Time.time;
                   settedPosition = false;
               }
               if (Time.time - startTime > 3f)
               {
                   Debug.Log("Dropped");
                   selection.gameObject.layer = 0;
                   selection.gameObject.GetComponent<Sphere>().useGravity = true;
                   couroutineRunnigR = false;
                   couroutineMovingR = false;
                   yield break;
               }
               yield return new WaitForSeconds(0.01f);
           }
           yield break;

       }
       else
       {
           while (couroutineMovingL)
           {
               Debug.Log("inMoveSphere loop");
               if (!settedPosition)
               {
                   prev_position = selection.position;
                   settedPosition = true;
               }
               selection.gameObject.layer = 2;
               selection.gameObject.GetComponent<Sphere>().useGravity = false;
               selection.position = hitPointL;
               if (Vector3.Distance(prev_position, selection.position) > 50)
               {
                   startTime = Time.time;
                   settedPosition = false;
               }
               if (Time.time - startTime > 3f)
               {
                   Debug.Log("Dropped");
                   selection.gameObject.layer = 0;
                   selection.gameObject.GetComponent<Sphere>().useGravity = true;
                   couroutineRunnigL = false;
                   couroutineMovingL = false;
                   yield break;
               }
               yield return new WaitForSeconds(0.01f);
           }
           yield break;
       }

   }

   void HighlightSphere(Transform _selection, string objectName)
   {
       if (objectName.Contains("Sphere"))
       {
           var selectionRenderer = _selection.GetComponent<Renderer>();
           if (selectionRenderer != null)
           {
               selectionRenderer.material = highlightMaterial;
           }
       }
   }

   void DeHighlightSphere(Transform _selection, string objectName)
   {
       if (objectName.Contains("Sphere"))
       {
           var selectionRenderer = _selection.GetComponent<Renderer>();
           selectionRenderer.material = defaultMaterial;
           _selection = null;
       }
   }
   */

}
