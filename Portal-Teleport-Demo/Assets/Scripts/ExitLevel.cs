using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            int thisScene = SceneManager.GetActiveScene().buildIndex;
            thisScene += 1;

            if (thisScene > SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Exit Application");
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(thisScene);
            }
        }
    }
}
