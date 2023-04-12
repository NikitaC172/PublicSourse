using UnityEngine;
using DG.Tweening;

public class AttackState : State
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 0.5f;

    private float _offset = 1.0f;
    private Vector3 _fastMove;

    private void OnEnable()
    {
        _fastMove = new Vector3(0, -_offset, _offset);
        Invoke(nameof(ChangeFreeze), 1.2f);
    }

    private void FixedUpdate()
    {        
        _rigidbody.AddRelativeForce(_fastMove * _speed, ForceMode.VelocityChange);

    }

    private void Update()
    {        
        transform.DOLookAt(_target.transform.position, 0, AxisConstraint.Y);
    }

    private void ChangeFreeze()
    {
        float speedReducer = 3;
        _rigidbody.freezeRotation=false;
        _speed /= speedReducer;
    }
}
