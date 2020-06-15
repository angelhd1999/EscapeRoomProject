using UnityEngine;
/// <summary>
/// This script is used to change between scenes and closing the game thanks to the head collider.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject GameStateManager = null;

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
                Debug.Log("Exiting");
                GameStateManager.GetComponent<GameStateManager>().ExitGame();
            }

        }
    }
}
