using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    public Transform GetPoint()
    {
        return _points[Random.Range(0, _points.Count)];
    }
}
