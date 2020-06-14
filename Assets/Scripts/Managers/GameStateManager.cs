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
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject openRock;
    [SerializeField] private GameObject fallenRocks;
    [SerializeField] private GameObject sun;

    private bool isSphereScene = true;
    private bool initDone = false;
    private bool lookingMessage = false;
    private bool trackingEnabled = false;
    private Animator poseAnim;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poseAnim = pose.GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "SpheresScene")
        {
            isSphereScene = true;
            initSpheresScene();
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
                if (poseAnim.GetCurrentAnimatorStateInfo(0).IsName("FirstInterState") && !lookingMessage)
                {
                    LookMessage();
                    lookingMessage = true;
                }
                else if(lookingMessage)
                {
                    if (message.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MsgInterState"))
                    {
                        if (!trackingEnabled)
                        {
                            trackingEnabled = true;
                            pose.GetComponent<TrackingReceiver>().enabled = true; //Start posenet      
                            pose.GetComponent<RetryGesture>().enabled = true;
                        }
                        
                        if (pose.GetComponent<RetryGesture>().wannaExit)
                        {
                            pose.GetComponent<TrackingReceiver>().enabled = false; //Start posenet      
                            pose.GetComponent<RetryGesture>().enabled = false;
                            head.position = new Vector3(0, 180, 0);
                            lWrist.position = new Vector3(-120, 0, 0);
                            rWrist.position = new Vector3(120, 0, 0);
                            message.GetComponent<Animator>().SetBool("endMessageFade", true);
                            background.GetComponent<Animator>().SetBool("endBackgroundFade", true);
                            poseAnim.SetBool("Escape", true);
                            initDone = true;
                        }
                        else if (pose.GetComponent<RetryGesture>().wannaStay)
                        {
                            pose.GetComponent<RetryGesture>().enabled = false;
                            message.GetComponent<Animator>().SetBool("endMessageFade", true);
                            background.GetComponent<Animator>().SetBool("endBackgroundFade", true);
                            poseAnim.SetBool("Stay", true);
                            fallenRocks.SetActive(true);
                            initDone = true;
                        }
                    }
                }
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
        poseAnim.SetBool("InitScene1", true);  
    }

    public void LookMessage()
    {
        message.GetComponent<Animator>().SetBool("startMessageFade", true);
        background.GetComponent<Animator>().SetBool("startBackgroundFade", true);
        
    }

    public void preTubesScene()
    {
        pose.GetComponent<TrackingReceiver>().enabled = false; //Stop posenet
        poseAnim.SetBool("SceneOneDone", true);
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
        poseAnim.SetBool("StartTubes", true);
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
        poseAnim.SetBool("WalkingToSun", true);
        openRock.GetComponent<Animator>().SetBool("SecondColor", true);
        sun.GetComponent<Animator>().SetBool("StartFlare", true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

