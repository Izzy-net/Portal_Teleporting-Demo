using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BatPath", menuName = "Scriptable Objects/BatPath")]
public class BatPath : ScriptableObject
{
    [SerializeField] List<Transform> pathPoints = new List<Transform>();
}
