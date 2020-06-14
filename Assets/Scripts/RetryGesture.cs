using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RetryGesture : MonoBehaviour
{
    public GameObject head;
    public GameObject UI;
    public bool canRetry = false;
    public bool wannaExit = false;
    public bool wannaStay = false;

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

    // Update is called once per frame
    void Update()
    {
        //if (canRetry)
        //{
        //    canRetry = false;
            
        //}
    }

    private IEnumerator InitPosition()
    {
        while (true)
        {
            yesInitPosition = head.transform.position.y;
            noInitPosition = head.transform.position.x;
            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator UpYes()
    {
        while(!upMade) 
        { 
            if (head.transform.position.y > yesInitPosition + 20)
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
            if (head.transform.position.y < yesInitPosition - 30)
            {
                downMade = true;
                wannaStay = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator LeftNo()
    {
        while (!leftMade)
        {
            if (head.transform.position.x < noInitPosition - 50)
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
            if (head.transform.position.x > noInitPosition + 50)
            {
                rightMade = true;
                wannaExit = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


}