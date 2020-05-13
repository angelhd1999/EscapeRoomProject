using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RetryGesture : MonoBehaviour
{
    public GameObject head;
    public GameObject UI;
    public bool canRetry = false;

    private float yesInitPosition;
    private float noInitPosition;
    private bool upMade = false;
    private bool downMade = false;
    private bool rightMade = false;
    private bool leftMade = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canRetry)
        {
            canRetry = false;
            StartCoroutine("InitPosition");
            StartCoroutine("UpYes");
            StartCoroutine("LeftNo");
        }
    }

    private IEnumerator InitPosition()
    {
        while (true)
        {
            yesInitPosition = head.transform.position.y;
            noInitPosition = head.transform.position.x;
            yield return new WaitForSeconds(2.0f);
        }
    }

    private IEnumerator UpYes()
    {
        while(!upMade) 
        { 
            if (head.transform.position.y > yesInitPosition + 50)
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
            if (head.transform.position.y < yesInitPosition - 50)
            {
                downMade = true;
                Debug.Log("Affirmative! --> Restart");
                UI.GetComponent<UIManager>().RestartGame();
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
                Debug.Log("Negative! --> Exit");
                UI.GetComponent<UIManager>().ExitGame();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


}