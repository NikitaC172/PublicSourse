using System.Collections;
using UnityEngine;

public class JumpMover : MonoBehaviour, IMovable
{
    [SerializeField] private float _timeMove;
    [SerializeField] private float _endPointZ = -10f;

    public void Move()
    {
        StartCoroutine(MoveToPoint());
    }

    private IEnumerator MoveToPoint()
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 finishpoint = new Vector3(transform.position.x, transform.position.y, _endPointZ);

        while (CheckControlePoint() == true)
        {
            transform.position = Vector3.Slerp(startPosition, finishpoint, time / _timeMove);
            time += Time.deltaTime;
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
