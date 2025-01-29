using UnityEngine;

[CreateAssetMenu(fileName = "PortalSpawnPoints", menuName = "Scriptable Objects/PortalSpawnPoints")]
public class PortalSpawnPoints : ScriptableObject
{
    [SerializeField] UnityEngine.Vector3 spawnCoordinates = new UnityEngine.Vector3 (0f, 0f);
    [SerializeField] string exitScene;
    [SerializeField] string enterScene;

    public UnityEngine.Vector3 GetSpawnCoordinates()
    {
        return spawnCoordinates;
    }

    public string GetExitScene()
    {
        return exitScene;
    }

    public string GetEnterScene()
    {
        return enterScene;
    }
}
