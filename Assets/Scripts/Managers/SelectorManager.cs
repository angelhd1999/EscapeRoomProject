using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    [SerializeField] private GameObject pose;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private float timeToPick = 0.75f;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_selectionR != null)
        {
            DeHighlightSphere(_selectionR, objectNameR);
        }
        if (_selectionL != null)
        {
            DeHighlightSphere(_selectionL, objectNameL);

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
               
                HighlightSphere(_selectionR, objectNameR);
                CheckPick(setRight);
            }
            else
            {
                objectNameR = hitR.collider.gameObject.name;
                hitPointR = hitR.point;
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

                HighlightSphere(_selectionL, objectNameL);
                CheckPick(setLeft);
            }
            else
            {
                objectNameL = hitL.collider.gameObject.name;
                hitPointL = hitL.point;
            }
        }
        else
        {
            objectNameL = "";
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristL.transform.position - Camera.main.transform.position) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
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
            if (Time.time - startTime > timeToPick)
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
            if (Time.time - startTime > timeToPick)
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
        if (objectName.Contains("Sphere")) {
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
}