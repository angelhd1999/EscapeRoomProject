using System;
using System.Collections.Generic;
using UnityEngine;

public class SmokeManager : MonoBehaviour
{
    [SerializeField] private GameObject pose = null;
    [SerializeField] private GameObject GameStateManager = null;
    [SerializeField] private string selectableTag = "Selectable";
    private Transform _selectionR = null;
    private Transform _selectionL = null;
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

    /*
     * Ball colors array
     * ballColors[0] --> red.
     * ballColors[1] --> green.
     * ballColors[2] --> blue.
     */
    private int[] ballColors;

    // Start is called before the first frame update
    void Start()
    {
        ballColors = new int[3];
        this.counter = 0; //Start counter at 0.
        this.changeBallColor();
    }

    //Debug


    void changeBallColor()
    {
        System.Random r = new System.Random();

        int[] smokeColors = smoke.GetComponent<SmokeScript>().getCurrentColor();

        ballColors[0] = r.Next(0, 256);
        ballColors[1] = r.Next(0, 256);
        ballColors[2] = r.Next(0, 256);

        while (ballColors[0] - colorComparationTolerance < smokeColors[0] && smokeColors[0] < ballColors[0] + colorComparationTolerance)
        {
            ballColors[0] = r.Next(0, 256);
        }

        while (ballColors[1] - colorComparationTolerance < smokeColors[1] && smokeColors[1] < ballColors[1] + colorComparationTolerance)
        {
            ballColors[1] = r.Next(0, 256);
        }

        while (ballColors[2] - colorComparationTolerance < smokeColors[2] && smokeColors[2] < ballColors[2] + colorComparationTolerance)
        {
            ballColors[2] = r.Next(0, 256);
        }

        sphere.GetComponent<ColorBall>().setColor(ballColors[0], ballColors[1], ballColors[2]);
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
                if (selectedCrankRight != null && !selectedCrankRight.name.Equals(objectNameR))
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
                if (selectedCrankRight != null)
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
            if (selectedCrankRight != null)
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
        if (smokeBallEqualColor() || Input.GetKeyDown("space")) //To Debug
        //if (smokeBallEqualColor())
        {
            counter++; //Add one to the counter.
            if (counter == 1)
            {
                changeBallColor(); //Change ball color.
                GameStateManager.GetComponent<GameStateManager>().preEndTubesScene();
            }
            if (counter == 2)
            {
                GameStateManager.GetComponent<GameStateManager>().endTubesScene();
            }
        }
    }

    void rotateCrank(GameObject crank, Vector3 rotationVector, string hand)
    {
        crank.GetComponent<RotateObject>().setVector(rotationVector);
        int[] smokeColors = smoke.GetComponent<SmokeScript>().getCurrentColor();

        //Switch with different case for each crank.
        switch (crank.name)
        {
            case "Red Cross":
                if ((String.Equals(hand, "right") && smokeColors[0] >= 254) || (String.Equals(hand, "left") && smokeColors[0] == 0))
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
                if ((String.Equals(hand, "right") && smokeColors[1] >= 254) || (String.Equals(hand, "left") && smokeColors[1] == 0))
                {
                    crank.GetComponent<RotateObject>().enabled = false;
                }
                else
                {
                    crank.GetComponent<RotateObject>().enabled = true;
                }
                if (String.Equals(hand, "right")) smoke.GetComponent<SmokeScript>().increaseColor(0.0f, colorChangeTolerance, 0.0f);
                else smoke.GetComponent<SmokeScript>().increaseColor(0.0f, -colorChangeTolerance, 0.0f);
                break;
            case "Blue Cross":
                if ((String.Equals(hand, "right") && smokeColors[2] >= 254) || (String.Equals(hand, "left") && smokeColors[2] == 0))
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

        if (!crank.GetComponent<AudioSource>().isPlaying)
        {
            crank.GetComponent<AudioSource>().Play();
        }
    }

    //Function that checks if the ball has the same color as the smoke with certain tolerance.
    bool smokeBallEqualColor()
    {
        int colorCount = 0;

        int[] smokeColors = smoke.GetComponent<SmokeScript>().getCurrentColor();

        if (ballColors[0] - colorComparationTolerance < smokeColors[0] && smokeColors[0] < ballColors[0] + colorComparationTolerance)
        {
            Debug.Log("Red OK"); //Control log.
            colorCount++;
        }

        if (ballColors[1] - colorComparationTolerance < smokeColors[1] && smokeColors[1] < ballColors[1] + colorComparationTolerance)
        {
            Debug.Log("Green OK"); //Control log.
            colorCount++;
        }

        if (ballColors[2] - colorComparationTolerance < smokeColors[2] && smokeColors[2] < ballColors[2] + colorComparationTolerance)
        {
            Debug.Log("Blue OK"); //Control log.
            colorCount++;
        }

        //If three colors are inside tolerance return true.
        if (colorCount == 3) return true;
        else return false;
    }


}
