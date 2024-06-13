using UnityEngine;


public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private XRIDefaultInputActions _playerInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _force = 0.5f;

    private Vector3 _position;

    private void Awake()
    {
        _rigidbody.centerOfMass = Vector3.zero;
        _playerInput = new XRIDefaultInputActions();
        _playerInput.XRILeftHandLocomotion.Move.performed += ctx => Move();
        _playerInput.XRIHead.Rotation.performed += ctx => Look();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Look()
    {}

    private void Move()
    {
        Vector2 direction = _playerInput.XRILeftHandLocomotion.Move.ReadValue<Vector2>();
        float angelRotateCamera = _camera.transform.eulerAngles.y * Mathf.Deg2Rad;
        Vector2 forward = new Vector2(direction.y * Mathf.Sin(angelRotateCamera), direction.y * Mathf.Cos(angelRotateCamera));
        Vector2 side = new Vector2(direction.x * Mathf.Sin(angelRotateCamera + Mathf.PI / 2), direction.x * Mathf.Cos(angelRotateCamera + Mathf.PI / 2));
        Vector2 move = (forward + side) * _force;
        _rigidbody.AddForce(move.x, 0, move.y, ForceMode.VelocityChange);
        _position = new Vector3(_camera.transform.position.x, transform.position.y, _camera.transform.position.z);
        transform.position = _position;
    }
}
