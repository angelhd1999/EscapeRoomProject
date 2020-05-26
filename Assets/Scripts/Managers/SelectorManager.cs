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

    private Transform _selectionR;
    private Transform _selectionL;
    private string objectNameR;
    private string objectNameL;
    private bool couroutineRunnigR = false;
    private bool couroutineRunnigL = false;
    private bool right = true;
    private bool left = false;
    Vector3 hitPointR;

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
                hitPointR = hitR.point;
                HighlightSphere(_selectionR, objectNameR);
                CheckPick(right);
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
                CheckPick(left);
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
        while (true)
        {
            if (firstObjectName != objectNameR)
            {
                couroutineRunnigR = false;
                yield break;
            }
            if (Time.time - startTime > 3f )
            {
                Debug.Log("Achieved" + firstObjectName);
                StartCoroutine(MoveSphere(firstObjectSelected, right));
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator PickSphereL()
    {
        string firstObjectName = objectNameL;
        Transform firstObjectSelected = _selectionL;
        //Debug.Log(firstObjectName);
        float startTime = Time.time;
        while (true)
        {
            if (firstObjectName != objectNameL)
            {
                couroutineRunnigL = false;
                yield break;
            }
            if (Time.time - startTime > 3f)
            {
                Debug.Log("Achieved" + firstObjectName);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MoveSphere(Transform selection, bool right)
    {
        while (true)
        {
            if (right)
            {
                selection.gameObject.layer = 2;
                selection.gameObject.GetComponent<Sphere>().useGravity = false;
                selection.position = hitPointR;
            }
            else
            {

            }
            yield return new WaitForSeconds(0.001f);
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