using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    private GameManager gameManagerScript;
    [SerializeField] List<PortalSpawnPoints> spawnPoints = new List<PortalSpawnPoints>();
    private PortalSpawnPoints portalSpawnPointSO;
    private Vector3 playerCoordinates;
    private GameObject player;
    private BoxCollider2D portalCollider;

    private void Awake() 
    {
        player = FindFirstObjectByType<PlatformerPlayer>().gameObject;
        portalCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        gameManagerScript = FindAnyObjectByType<GameManager>();

        if (gameManagerScript.GetIsPortalling())
        {
            player.transform.position = gameManagerScript.GetLoadCoordinates();

            if (!SceneManager.GetActiveScene().name.Contains("Sub"))
            {
                portalCollider.enabled = false;
                gameManagerScript.LoadSceneInfo();
            }

            gameManagerScript.SetIsPortalling(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        if (!SceneManager.GetActiveScene().name.Contains("Sub"))
        {
            gameManagerScript.SaveSceneInfo();
        }
        gameManagerScript.SetIsPortalling(true);

        string currentScene = SceneManager.GetActiveScene().name;
        PortalSpawnPoints transitionSpawnObject = spawnPoints.Find(portalSpawnPointSO => portalSpawnPointSO.GetExitScene() == currentScene);

        string sceneToLoad = transitionSpawnObject.GetEnterScene();
        playerCoordinates = transitionSpawnObject.GetSpawnCoordinates();

        FindAnyObjectByType<GameManager>().SetLoadCoordinates(playerCoordinates);
        FindAnyObjectByType<GameManager>().SetSceneEntering(sceneToLoad);

        SceneManager.LoadScene(sceneToLoad);
    }
}
