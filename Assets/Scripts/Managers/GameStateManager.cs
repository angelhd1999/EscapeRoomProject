﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance; //Singleton
    
    [SerializeField] private GameObject pose;
    [SerializeField] private Transform head;
    [SerializeField] private Transform lWrist;
    [SerializeField] private Transform rWrist;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject openRock;
    [SerializeField] private GameObject sun;

    private bool isSphereScene = true;
    private bool initDone = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SpheresScene")
        {
            isSphereScene = true;
        } else
        {
            isSphereScene = false;
            initTubesScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SpheresScene");
        }
        if (!initDone)
        {
            if (isSphereScene)
            {
                //
            }
            else
            {
                if (pose.transform.position.z == 0f)
                {
                    startTubesScene();
                    initDone = true;
                }
            }
        }
           
        
    }

    public void initSpheresScene()
    {
        pose.GetComponent<Animator>().SetBool("FirstScene", true);
        
    }

    public void preTubesScene()
    {
        pose.GetComponent<TrackingReceiver>().enabled = false; //Stop posenet
        pose.GetComponent<Animator>().SetBool("SceneOneDone", true);
        //Make earthquake sound
        head.position = new Vector3(0, 180, 0);
        lWrist.position = new Vector3(-120, 0, 0);
        rWrist.position = new Vector3(120, 0, 0);
        particles.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
    public void goTubesScene()
    {
        SceneManager.LoadScene("TubesScene");
    }

    public void initTubesScene()
    {
        pose.GetComponent<Animator>().SetBool("StartTubes", true);
        GetComponent<AudioSource>().Play();
    }

    public void startTubesScene()
    {
        pose.GetComponent<TrackingReceiver>().enabled = true; //Start posenet
        particles.gameObject.SetActive(false);
    }

    public void preEndTubesScene()
    {
        openRock.GetComponent<Animator>().SetBool("FirstColor", true);
    }


    public void endTubesScene()
    {
        pose.GetComponent<TrackingReceiver>().enabled = false;
        head.position = new Vector3(0, 180, 0);
        lWrist.position = new Vector3(-120, 0, 0);
        rWrist.position = new Vector3(120, 0, 0);
        pose.GetComponent<Animator>().SetBool("WalkingToSun", true);
        openRock.GetComponent<Animator>().SetBool("SecondColor", true);
        sun.GetComponent<Animator>().SetBool("StartFlare", true);
    }


}

