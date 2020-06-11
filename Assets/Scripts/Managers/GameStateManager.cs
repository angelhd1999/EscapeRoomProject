using System.Collections;
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

    public AudioSource animationSound;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void preTubesScene()
    {
        pose.GetComponent<TrackingReceiver>().enabled = false; //Stop posenet
        pose.GetComponent<Animator>().SetBool("SceneOneDone", true);
        //Make earthquake sound
        animationSound.Play();
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
}

