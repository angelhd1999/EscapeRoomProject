using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject GameStateManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SceneChange"))
        {
            if(other.name == "Tubes")
            {
                GameStateManager.GetComponent<GameStateManager>().goTubesScene();
            }
            if (other.name == "Exit")
            {
                GameStateManager.GetComponent<GameStateManager>().ExitGame();
            }

        }
    }
}
