using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    [SerializeField] private GameObject pose;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //    if(_selection != null)
        //    {
        //        var selectionRenderer = _selection.GetComponent<Renderer>();
        //        selectionRenderer.material = defaultMaterial;
        //        _selection = null;
        //    }
        //    //var ray = Camera.main.ScreenPointToRay(pose.GetComponent<TrackingReceiver>().wristR.transform.position); //"original"
        //    //var ray = Physics.Raycast(Camera.main.transform.position, pose.GetComponent<TrackingReceiver>().wristR.transform.position);
        //    RaycastHit hit;
        //    //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        //    //if (Physics.Raycast(ray, out hit)) //original
        //    if (Physics.Raycast(Camera.main.transform.position, pose.GetComponent<TrackingReceiver>().wristR.transform.position, out hit, Mathf.Infinity))
        //    {
        //        Debug.DrawRay(new Vector3(0f, 0f, 540f), pose.GetComponent<TrackingReceiver>().wristR.transform.position * Mathf.Infinity, Color.yellow);
        //        var selection = hit.transform;
        //        if (selection.CompareTag(selectableTag))
        //        { 
        //            var selectionRenderer = selection.GetComponent<Renderer>();
        //            if (selectionRenderer != null)
        //            {
        //                selectionRenderer.material = highlightMaterial;
        //            }

        //            _selection = selection;
        //        }
        //    }
        //    else
        //    {
        //        Debug.DrawRay(Camera.main.transform.position, pose.GetComponent<TrackingReceiver>().wristR.transform.position * Mathf.Infinity, Color.white);
        //    }
        //}
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position) * 1000, Color.yellow);
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, (pose.GetComponent<TrackingReceiver>().wristR.transform.position - Camera.main.transform.position) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}