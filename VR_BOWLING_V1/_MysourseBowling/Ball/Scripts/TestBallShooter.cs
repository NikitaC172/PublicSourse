using UnityEngine;

public class TestBallShooter : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [Range(0, 1000)][SerializeField] private float _force;
    [SerializeField] private bool _isActivate = false;

    private void FixedUpdate()
    {
        if (_isActivate == true)
        {
            _isActivate = false;
            _rigidbody.AddForce(Vector3.forward * -_force);
        }
    }
}
