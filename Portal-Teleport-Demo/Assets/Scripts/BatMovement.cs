using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] Transform path;
    List<Transform> pathPoints = new List<Transform>();
    int listIndex = 0;
    Transform targetPoint;
    [SerializeField] float moveSpeed = 1f;

    private void Awake() 
    {
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

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)  //Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.5f
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

    private void OnDrawGizmos() 
    {
        foreach (Transform child in path)
        {
            pathPoints.Add(child);
        }

        for (int i = 0; i < pathPoints.Count; i++)
        {
            Gizmos.DrawWireSphere(pathPoints[i].position, 0.5f);
            if (i > 0)
            {
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i-1].position);
            }
        }  
    }
}
