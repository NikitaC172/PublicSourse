using UnityEngine;
using DG.Tweening;

public class PigeonTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _timeRotate = 1.5f;

    private void Update()
    {
        transform.DOLookAt(_target.position, _timeRotate, AxisConstraint.Y, Vector3.up).Duration(true);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
