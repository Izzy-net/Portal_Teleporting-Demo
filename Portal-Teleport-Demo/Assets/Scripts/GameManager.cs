using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField] List<BatMovement> enemyAliveRef = new List<BatMovement>();
    [SerializeField] List<string> enemiesStillAlive = new List<string>();
    [SerializeField] List<string> enemyCurrent = new List<string>();
    [SerializeField] UnityEngine.Vector3 loadCoordinates = new UnityEngine.Vector3 (10f, 0f, 0f);
    [SerializeField] bool isPortalling;
    [SerializeField] string sceneEntering;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetLoadCoordinates(UnityEngine.Vector3 value)
    {
        loadCoordinates = value;
    }

    public UnityEngine.Vector3 GetLoadCoordinates()
    {
        return loadCoordinates;
    }

    public void SetIsPortalling(bool value)
    {
        isPortalling = value;
    }

    public bool GetIsPortalling()
    {
        return isPortalling;
    }

    public void SetSceneEntering(string value)
    {
        sceneEntering = value;
    }

    public string GetSceneEntering()
    {
        return sceneEntering;
    }

    public void SaveSceneInfo()
    {
        enemyAliveRef = FindObjectsByType<BatMovement>(FindObjectsSortMode.None).ToList();
        for (int i = 0; i < enemyAliveRef.Count; i++)
        {
            enemiesStillAlive.Add(enemyAliveRef[i].name);
        }
    }

    public void LoadSceneInfo()
    {
        var enemies = FindObjectsByType<BatMovement>(FindObjectsSortMode.None).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyCurrent.Add(enemies[i].name);
        }
    
        var enemiesToDelete = enemyCurrent.Except(enemiesStillAlive).ToList();

        for (int i = 0; i < enemiesToDelete.Count; i++)
        {
            Destroy(GameObject.Find(enemiesToDelete[i]));
        }
    }
}
