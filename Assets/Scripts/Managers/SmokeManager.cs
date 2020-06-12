using System;
using System.Collections.Generic;
using UnityEngine;

public class SmokeManager : MonoBehaviour
{
    [SerializeField] private GameObject pose;
    [SerializeField] private string selectableTag = "Selectable";

    private Transform _selectionR;
    private Transform _selectionL;
    private string objectNameR;
    private string objectNameL;
    Vector3 hitPointR;
    Vector3 hitPointL;

    /*Selected cranks*/
    private GameObject selectedCrankRight = null;
    private GameObject selectedCrankLeft = null;

    public float colorChangeTolerance; //Velocity where color changes.
    public int colorComparationTolerance; //Tolerance to compare smoke and ball color.
    public GameObject smoke;
    public GameObject sphere;

    private int counter; //Color achieved counter.

    Dictionary<string, int> ballColors = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        this.counter = 0; //Start counter at 0.
        this.changeBallColor();
    }

    void changeBallColor()
    {
        System.Random r = new System.Random();

        ballColors["red"] = r.Next(0, 256);
        ballColors["green"] = r.Next(0, 256);
        ballColors["blue"] = r.Next(0, 256);

        sphere.GetComponent<ColorBall>().setColor(ballColors["red"], ballColors["green"], ballColors["blue"]);
    }

    // Update is called once per frame
    void Update()
    {
        //Right Wrist
        RaycastHit hitR;
        // The ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position), out hitR, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position) * 1000, Color.yellow);
            var selection = hitR.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selectionR = selection;
                objectNameR = hitR.collider.gameObject.name;

                //Assing selected crank.
                if(selectedCrankRight != null && !selectedCrankRight.name.Equals(objectNameR))
                {
                    selectedCrankRight.GetComponent<RotateObject>().enabled = false;
                    selectedCrankRight = null;
                }
                selectedCrankRight = hitR.collider.gameObject;
                //RotateCrank.
                rotateCrank(selectedCrankRight, new Vector3(0, 0, -1), "right");
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
            if(selectedCrankRight != null)
                {
                selectedCrankRight.GetComponent<RotateObject>().enabled = false;
                selectedCrankRight = null;
            }
            objectNameR = "";
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position) * 1000, Color.white);
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
                _selectionL = selection;
                objectNameL = hitL.collider.gameObject.name;

                //Assing selected crank.
                if (selectedCrankLeft != null && !selectedCrankLeft.name.Equals(objectNameL))
                {
                    selectedCrankLeft.GetComponent<RotateObject>().enabled = false;
                    selectedCrankLeft = null;
                }
                selectedCrankLeft = hitL.collider.gameObject;
                //RotateCrank.
                rotateCrank(selectedCrankLeft, new Vector3(0, 0, 1), "left");
            }
            else
            {
                if (selectedCrankLeft != null)
                {
                    selectedCrankLeft.GetComponent<RotateObject>().enabled = false;
                    selectedCrankLeft = null;
                }
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
        }

        //If the smoke and ball color are equal.
        if (smokeBallEqualColor())
        {
            counter++; //Add one to the counter.
            changeBallColor(); //Change ball color.
        }
    }

    void rotateCrank(GameObject crank, Vector3 rotationVector, string hand)
    {
        crank.GetComponent<RotateObject>().setVector(rotationVector);
        Dictionary<string, int> smokeColors = smoke.GetComponent<SmokeScript>().getCurrentColor();
        //What crank was selected.
        switch (crank.name)
        {
            case "Red Cross":
                if ((String.Equals(hand, "right") && smokeColors["red"] >= 254) || (String.Equals(hand, "left") && smokeColors["red"] == 0))
                {
                    crank.GetComponent<RotateObject>().enabled = false;
                }
                else
                {
                    crank.GetComponent<RotateObject>().enabled = true;
                }
                if (String.Equals(hand, "right")) smoke.GetComponent<SmokeScript>().increaseColor(colorChangeTolerance, 0.0f, 0.0f);
                else smoke.GetComponent<SmokeScript>().increaseColor(-colorChangeTolerance, 0.0f, 0.0f);
                break;
            case "Green Cross":
                Debug.Log("Color " + smokeColors["green"]);
                if ((String.Equals(hand, "right") && smokeColors["green"] >= 254) || (String.Equals(hand, "left") && smokeColors["green"] == 0))
                {
                    crank.GetComponent<RotateObject>().enabled = false;
                }
                else
                {
                    crank.GetComponent<RotateObject>().enabled = true;
                }
                if(String.Equals(hand, "right")) smoke.GetComponent<SmokeScript>().increaseColor(0.0f, colorChangeTolerance, 0.0f);
                else smoke.GetComponent<SmokeScript>().increaseColor(0.0f, -colorChangeTolerance, 0.0f);
                break;
            case "Blue Cross":
                if ((String.Equals(hand, "right") && smokeColors["blue"] >= 254) || (String.Equals(hand, "left") && smokeColors["blue"] == 0))
                {
                    crank.GetComponent<RotateObject>().enabled = false;
                }
                else
                {
                    crank.GetComponent<RotateObject>().enabled = true;
                }
                if (String.Equals(hand, "right")) smoke.GetComponent<SmokeScript>().increaseColor(0.0f, 0.0f, colorChangeTolerance);
                else smoke.GetComponent<SmokeScript>().increaseColor(0.0f, 0.0f, -colorChangeTolerance);
                break;
            default:
                break;
        }
    }

    /* Instead of using this function a variable called hand was added to rotateCrank function.
    //Function to compare two Vector3 with an allowedDifference.
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
    */


    bool smokeBallEqualColor()
    {
        int colorCount = 0;

        Dictionary<string, int> smokeColors = smoke.GetComponent<SmokeScript>().getCurrentColor();

        if (ballColors["red"] - colorComparationTolerance < smokeColors["red"] && smokeColors["red"] < ballColors["red"] + colorComparationTolerance)
        {
            //Debug.Log("Red OK"); //Control log.
            colorCount++;
        }

        if (ballColors["green"] - colorComparationTolerance < smokeColors["green"] && smokeColors["green"] < ballColors["green"] + colorComparationTolerance)
        {
            //Debug.Log("Green OK"); //Control log.
            colorCount++;
        }

        if (ballColors["blue"] - colorComparationTolerance < smokeColors["blue"] && smokeColors["blue"] < ballColors["blue"] + colorComparationTolerance)
        {
            //Debug.Log("Blue OK"); //Control log.
            colorCount++;
        }

        //If three colors are inside tolerance return true.
        if (colorCount == 3) return true;
        else return false;
    }

   
}
