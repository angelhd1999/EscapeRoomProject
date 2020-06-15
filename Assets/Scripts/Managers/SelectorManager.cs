using System.Collections;
using UnityEngine;

/// <summary>
/// This script manages the user interaction with the spheres of the first scene, you can grab and release the spheres and it will be highlighted when your hand is over them.
/// </summary>
public class SelectorManager : MonoBehaviour
{
    [SerializeField] private GameObject pose = null;
    [SerializeField] private GameObject GameStateManager = null;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial = null;
    [SerializeField] private Material defaultMaterial = null;
    [SerializeField] private Material MgMaterial = null;
    [SerializeField] private Material AlMaterial = null;
    [SerializeField] private Material TiMaterial = null;
    [SerializeField] private Material BrMaterial = null;
    [SerializeField] private Material BzMaterial = null;
    [SerializeField] private Material AgMaterial = null;
    [SerializeField] private float timeToPick = 0.75f;
    [SerializeField] private float timeToDrop = 1f;
    [SerializeField] private bool couroutineRunnigR = false;

    private Material currentMaterial;
    private Transform _selectionR;
    private Transform _selectionL;
    private string objectNameR;
    private string objectNameL;
    private bool couroutineRunnigL = false;
    private bool couroutineMovingR = false;
    private bool couroutineMovingL = false; 
    private bool setRight = true;
    private bool setLeft = false;
    Vector3 hitPointR;
    Vector3 hitPointL;

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
        else //Raycast is not detecting collision
        {
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

                HighlightSphere(_selectionL, objectNameL);
                CheckPick(setLeft);
            }
            else
            {
                objectNameL = hitL.collider.gameObject.name;
                hitPointL = hitL.point;
            }
        }
        else //Raycast is not detecting collision
        {
            objectNameL = "";
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristL.transform.position - Camera.main.transform.position) * 1000, Color.white);
        }
    }

    //Checks if a hand is already picking an object before trying to pick it.
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

    //Makes the right hand pick an sphere if the hand is over it during 0.75s, if the hands detects another object it will stop trying to pick the sphere.
    IEnumerator PickSphereR()
    {
        string firstObjectName = objectNameR;
        Transform firstObjectSelected = _selectionR;
        if (firstObjectName == "Gold")
        {
            Debug.Log("Achieved");
            //Make sound of pick gold.
            firstObjectSelected.gameObject.GetComponent<AudioSource>().Play();
            _selectionR.gameObject.SetActive(false);
            GameStateManager.GetComponent<GameStateManager>().preTubesScene();
            this.gameObject.SetActive(false);
        }
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
                //Debug.Log("Achieved" + firstObjectName);
                CheckMove(firstObjectSelected, setRight);
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    //Makes the left hand pick an sphere if the hand is over it during 0.75s, if the hands detects another object it will stop trying to pick the sphere.
    IEnumerator PickSphereL()
    {
        string firstObjectName = objectNameL;
        Transform firstObjectSelected = _selectionL;
       
        float startTime = Time.time;
        if (firstObjectName == "Gold")
        {
            Debug.Log("Achieved");
            //Make sound of pick gold.
            firstObjectSelected.gameObject.GetComponent<AudioSource>().Play();
            _selectionR.gameObject.SetActive(false);
            GameStateManager.GetComponent<GameStateManager>().preTubesScene();
            this.gameObject.SetActive(false);

        }
        while (couroutineRunnigL)
        {
            if (firstObjectName != objectNameL)
            {
                couroutineRunnigL = false;
                yield break;
            }
            if (Time.time - startTime > timeToPick)
            {
                //Debug.Log("Achieved" + firstObjectName);
                CheckMove(firstObjectSelected, setLeft);
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    //Once the object is picked a sound will be played and thanks to the new couroutines the sphere will start moving along the hand.
    void CheckMove(Transform selection, bool hand)
    {
        if (hand)
        {
            if (!couroutineMovingR)
            {
                couroutineMovingR = true;
                selection.gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(MoveSphere(selection, hand));
            }
        }
        else
        {
            if (!couroutineMovingL)
            {
                couroutineMovingL = true;
                selection.gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(MoveSphere(selection, hand));
            }
        }
    }

    //Checks what hand is picking the object and make the object move along the hand until the hand stops for 1 second.
    IEnumerator MoveSphere(Transform selection, bool right)
    {
        float startTime = Time.time;
        bool settedPosition = false;
        Vector3 prev_position = selection.position;
        if (right)
        {
            while (couroutineMovingR)
            {
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
                if (Time.time - startTime > timeToDrop)
                {
                    //Debug.Log("Dropped");
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
                //Debug.Log("inMoveSphere loop");
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
                if (Time.time - startTime > timeToDrop)
                {
                    //Debug.Log("Dropped");
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

    //Highlights the selected sphere to make the user interaction easier.
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

    //Return the sphere to its original material.
    void DeHighlightSphere(Transform _selection, string objectName)
    {
        if (objectName.Contains("Sphere"))
        {
            switch (objectName)
            {
                case "Sphere (Mg)":
                    currentMaterial = MgMaterial;
                    break;
                case "Sphere (Al)":
                    currentMaterial = AlMaterial;
                    break;
                case "Sphere (Ti)":
                    currentMaterial = TiMaterial;
                    break;
                case "Sphere (Br)":
                    currentMaterial = BrMaterial;
                    break;
                case "Sphere (Bz)":
                    currentMaterial = BzMaterial;
                    break;
                case "Sphere (Ag)":
                    currentMaterial = AgMaterial;
                    break;
                default:
                    currentMaterial = defaultMaterial;
                    break;
            }
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = currentMaterial;
            _selection = null;
        }
    }
}