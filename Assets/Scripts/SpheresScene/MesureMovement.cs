using UnityEngine;
using System;

/// <summary>
/// This scripts make the measure bars of the safe move accordingly to the weights detected from the weight scales.
/// </summary>
public class MesureMovement : MonoBehaviour
{
    public GameObject LMesure;
    public GameObject RMesure;
    public GameObject LWScale;
    public GameObject RWScale;
    public GameObject Door;

    private float LWeight;
    private float RWeight;
    private float totalWeight;
    private int totalSpheres;
    private Vector3 initPositionLM;
    private Vector3 initPositionRM;
    private float moveDist = 26.5f;
    private float LRelation;
    private float RRelation;
    private bool pebbleDone = false;
    private bool runningScript = true;
    private int spheresNum = 5;

    // Start is called before the first frame update
    void Start()
    {
        initPositionLM = LMesure.transform.position + new Vector3(0, -moveDist, 0); //The bars will have the starting point reference at the bottom.
        initPositionRM = RMesure.transform.position + new Vector3(0, -moveDist, 0);
    }

    public void UpdateMesures()
    {
        if (runningScript)
        {
            totalSpheres = LWScale.GetComponent<WeightScale>().registeredRigidbodies + RWScale.GetComponent<WeightScale>().registeredRigidbodies;
            LWeight = LWScale.GetComponent<WeightScale>().calculatedMass;
            RWeight = RWScale.GetComponent<WeightScale>().calculatedMass;
            totalWeight = LWeight + RWeight;
            if (totalWeight != 0)
            {
                LRelation = LWeight / totalWeight;
                RRelation = 1f - LRelation;
                LMesure.transform.position = initPositionLM + new Vector3(0, LRelation * 2 * moveDist, 0); //The bars will start from the bottom, moveDist is half the distance of the total margin so it's multiplied by to to make the bars be able to reach the top.
                RMesure.transform.position = initPositionRM + new Vector3(0, RRelation * 2 * moveDist, 0);
            }
            else //If no weight is detected
            {
                LMesure.transform.position = initPositionLM + new Vector3(0, moveDist, 0); //The bars will reamain balanced at the middle.
                RMesure.transform.position = initPositionRM + new Vector3(0, moveDist, 0);
            }
            if (!pebbleDone)
            {
                PebbleFalling();
            }
            //If the two weight scales are balanced and all the spheres are detected the safe will be opened.
            if (Math.Round(LRelation, 4, MidpointRounding.AwayFromZero) == Math.Round(RRelation, 4, MidpointRounding.AwayFromZero) && totalSpheres == spheresNum)
            {
                runningScript = false;
                Door.GetComponent<Animator>().SetBool("PlaySafe", true);
                Door.GetComponent<AudioSource>().Play();
                //Door.GetComponent<Animator>().SetTrigger("OpenSafe"); Only necessary if culling mode is on Cull Completely.
            }
        }

    }

    //We added a pebble falling when the user puts two spheres over the weight scales to make the player feel that their interaction is causing changes on stage. (Earthquake prevention.)
    void PebbleFalling()
    {
        if (totalSpheres == 2)
        {
            pebbleDone = true;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
