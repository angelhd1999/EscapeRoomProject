using System.Collections;
using UnityEngine;

/// <summary>
/// This script is used at the starting animation to make the user decide between continue investigating the cave to find the treasure or escape from the cave with your colleages.
/// IMPORTANT: It sometimes need an effort to clearly show if you are saying yes or no.
/// </summary>
public class RetryGesture : MonoBehaviour
{
    
    public bool wannaExit = false;
    public bool wannaStay = false;

    [SerializeField] private GameObject head = null;
    [SerializeField] private float yesMargin = 30f;
    [SerializeField] private float noMargin = 50f;
    private float yesInitPosition;
    private float noInitPosition;
    private bool upMade = false;
    private bool downMade = false;
    private bool rightMade = false;
    private bool leftMade = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("InitPosition");
        StartCoroutine("UpYes");
        StartCoroutine("LeftNo");
    }

    //This function will be evalating the user's head position every 3 seconds to make this the starting point to the yes or no evaluation.
    private IEnumerator InitPosition()
    {
        while (true)
        {
            yesInitPosition = head.transform.position.y;
            noInitPosition = head.transform.position.x;
            yield return new WaitForSeconds(3.0f);
        }
    }

   //Check if the starting movement of yes, moving the head up is done, then it will call the function to check if the head is down.
    private IEnumerator UpYes()
    {
        while(!upMade) 
        { 
            if (head.transform.position.y > yesInitPosition + yesMargin)
            {
                upMade = true;
                yield return StartCoroutine("DownYes");

            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator DownYes()
    {
        while (!downMade)
        {
            if (head.transform.position.y < yesInitPosition - yesMargin)
            {
                downMade = true;
                wannaStay = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Check if the starting movement of no, moving the head left is done, then it will call the function to check if the head moving to right.
    private IEnumerator LeftNo()
    {
        while (!leftMade)
        {
            if (head.transform.position.x < noInitPosition - noMargin)
            {
                leftMade = true;
                yield return StartCoroutine("RightNo");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator RightNo()
    {
        while (!rightMade)
        {
            if (head.transform.position.x > noInitPosition + noMargin)
            {
                rightMade = true;
                wannaExit = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


}