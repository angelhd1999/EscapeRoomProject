using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitTubesScene : MonoBehaviour
{
    [SerializeField] private GameObject GameStateManager;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.GetComponent<GameStateManager>().initTubesScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z == 0f)
        {
            GameStateManager.GetComponent<GameStateManager>().startTubesScene();
            this.enabled = false;
        }
    }
}
