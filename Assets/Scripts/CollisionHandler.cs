using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
            {
                Debug.Log("Collided with Friendly object");
                break;
            }
            case "Finish":
            {
                Debug.Log("Collided with Finish object");
                break;
            }
            case "Fuel":
            {
                Debug.Log("Collided with Fuel object");
                break;
            }
            case "Ground":
            {
                Debug.Log("Collided with Ground");
                break;
            }
            default:
            {
                Debug.Log("Collided with obstacle");
                ReloadLevel();
                break;
            }

        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
