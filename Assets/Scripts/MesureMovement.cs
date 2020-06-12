﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        LWeight = LWScale.GetComponent<WeightScale>().calculatedMass;
        RWeight = RWScale.GetComponent<WeightScale>().calculatedMass;
        totalWeight = LWeight + RWeight;
        initPositionLM = LMesure.transform.position + new Vector3(0, -26.5f, 0);
        initPositionRM = RMesure.transform.position + new Vector3(0, -26.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (runningScript) 
        {
            totalSpheres = LWScale.GetComponent<WeightScale>().registeredRigidbodies + RWScale.GetComponent<WeightScale>().registeredRigidbodies;
            LWeight = LWScale.GetComponent<WeightScale>().calculatedMass;
            RWeight = RWScale.GetComponent<WeightScale>().calculatedMass;
            totalWeight = LWeight + RWeight;
            //Debug.Log(totalWeight);
            if (totalWeight != 0)
            {
                LRelation = LWeight / totalWeight;
                RRelation = 1f - LRelation;
                LMesure.transform.position = initPositionLM + new Vector3(0, LRelation * 2 * moveDist, 0);
                RMesure.transform.position = initPositionRM + new Vector3(0, RRelation * 2 * moveDist, 0);
            }
            else
            {
                LMesure.transform.position = initPositionLM + new Vector3(0, moveDist, 0);
                RMesure.transform.position = initPositionRM + new Vector3(0, moveDist, 0);
            }
            if(!pebbleDone)
            {
                PebbleFalling();
            }
            //Debug.Log("LRelation: " + Math.Round(LRelation, 4, MidpointRounding.AwayFromZero));
            //Debug.Log("RRelation: " + Math.Round(RRelation, 4, MidpointRounding.AwayFromZero));
            
            //if(true) //To test the transition.
            if (Math.Round(LRelation, 4, MidpointRounding.AwayFromZero) == Math.Round(RRelation, 4, MidpointRounding.AwayFromZero) && totalSpheres == spheresNum)
            {
                runningScript = false;
                Door.GetComponent<Animator>().SetBool("PlaySafe", true);
                Door.GetComponent<AudioSource>().Play();
                //Door.GetComponent<Animator>().SetTrigger("OpenSafe"); Only necessary if culling mode is on Cull Completely.
            }
        }
        
    }

    private void LateUpdate()
    {
        
    }

    void PebbleFalling()
    {
        if (totalSpheres == 2)
        {
            pebbleDone = true;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
