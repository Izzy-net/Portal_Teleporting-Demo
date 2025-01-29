using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] bool drawGizmosOn;
    [SerializeField] Transform pathPrefab;
    private Transform path;
    [SerializeField] Vector3 movePathLocation;
    List<Transform> pathPoints = new List<Transform>();
    List<Transform> pathGizmos = new List<Transform>();
    int listIndex = 0;
    Transform targetPoint;
    [SerializeField] float moveSpeed = 1f;

    private void Awake() 
    {
        CreatePath();

        if (movePathLocation != Vector3.zero)
        {
            path.position += movePathLocation;
        }

        foreach (Transform child in path)
        {
            pathPoints.Add(child);
        }
    }

    void Start()
    {
        targetPoint = pathPoints[listIndex].transform;
    }

    void Update()
    {
        Move(targetPoint);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            listIndex++;
            if (listIndex >= pathPoints.Count)
            {
                listIndex = 0;
            }
            targetPoint = pathPoints[listIndex].transform;
        }
    }

    void Move(Transform targetPoint)
    {
        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed);
    }

    private void CreatePath()
    {
        path = Instantiate(pathPrefab);
    }

    private void OnDrawGizmos() 
    {
        if (drawGizmosOn)
        {
            foreach (Transform child in pathPrefab)
            {
                pathGizmos.Add(child);
            }

            for (int i = 0; i < pathGizmos.Count; i++)
            {
                Gizmos.DrawWireSphere(pathGizmos[i].position + movePathLocation, 0.5f);
                if (i > 0)
                {
                    Gizmos.DrawLine(pathGizmos[i].position + movePathLocation, pathGizmos[i-1].position + movePathLocation);
                }
            }
        }
    }
}
