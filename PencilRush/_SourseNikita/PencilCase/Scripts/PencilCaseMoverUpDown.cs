using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PencilCase))]
[RequireComponent(typeof(PencilCaseMoverToPoint))]
public class PencilCaseMoverUpDown : MonoBehaviour
{
    [SerializeField] private PencilCaseMoverToPoint _moverToPoint;
    [SerializeField] private PencilCaseObjectModel _pencilCaseObject;
    [SerializeField] private float _timeMove = 3.5f;
    [SerializeField] private float _heightMove = 1.0f;

    private void OnValidate()
    {
        _moverToPoint = GetComponent<PencilCaseMoverToPoint>();
    }

    private void OnEnable()
    {
        _moverToPoint.Reached += StartMoveUpDown;
    }

    private void OnDisable()
    {
        _moverToPoint.Reached -= StartMoveUpDown;
    }

    private void StartMoveUpDown()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float time = 0;
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = new Vector3(transform.position.x, transform.position.y - _heightMove, transform.position.z);

        while (true)
        {
            time = 0;

            while (time < _timeMove)
            {
                time += Time.deltaTime;
                MoveToPoint(currentPosition, nextPosition, time / _timeMove);
                yield return null;
            }

            time = 0;

            while (time < _timeMove)
            {
                time += Time.deltaTime;
                MoveToPoint(nextPosition, currentPosition, time / _timeMove);
                yield return null;
            }
        }
    }

    private void MoveToPoint(Vector3 pointStart, Vector3 pointTarget, float normalizeTime)
    {
        _pencilCaseObject.transform.position = Vector3.Lerp(pointStart, pointTarget, normalizeTime);
    }
}
