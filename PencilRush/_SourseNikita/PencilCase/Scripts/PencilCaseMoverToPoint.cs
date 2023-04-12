using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PencilCase))]
public class PencilCaseMoverToPoint : MonoBehaviour
{
    [SerializeField] private PencilCase _pencilCase;
    [SerializeField] private LineSystemPencilCasePoint _lineSystemPencilCasePoint;
    [SerializeField] private ParticleSystem _particleSystemStars;
    [SerializeField] private float _timeMove = 5.0f;

    public Action Reached;

    private void OnValidate()
    {
        _lineSystemPencilCasePoint = FindObjectOfType<LineSystemPencilCasePoint>();
        _pencilCase = GetComponent<PencilCase>();
    }

    private void OnEnable()
    {
        _pencilCase.ShowedCase += StartMove;
    }

    private void OnDisable()
    {
        _pencilCase.ShowedCase -= StartMove;
    }

    private void StartMove()
    {
        StartCoroutine(MoveToPoint());
    }

    private IEnumerator MoveToPoint()
    {
        _particleSystemStars.Play();
        float time = 0;
        Vector3 currentPosition = transform.position;

        while (time < _timeMove)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, _lineSystemPencilCasePoint.transform.position, time / _timeMove);
            yield return null;
        }

        Reached?.Invoke();
    }
}
