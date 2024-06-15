using System.Collections;
using UnityEngine;

public class SimpleMover : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _endPointZ = -10f;

    public void Move()
    {
        StartCoroutine(MoveToPoint());
    }

    private IEnumerator MoveToPoint()
    {
        while (CheckControlePoint() == true)
        {
            transform.position = transform.position + transform.forward * _speed;
            yield return null;
        }
    }

    private bool CheckControlePoint()
    {
        if (transform.position.z > _endPointZ)
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }
}
