using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PencilAudioPlayer))]
public class PencilDroper : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private PoolObject _poolObject;
    [SerializeField] private PencilEffectsObject _pencilEffects;
    [SerializeField] private PencilAudioPlayer _pencilAudioPlayer;
    [SerializeField] private float _timeToTurnoff = 5.0f;
    [SerializeField] private float _forceImpulse = 5.0f;
    [SerializeField] private float _multiplicatorTorque = 0.5f;

    private bool _isDroped = false;
    private LinePositionSetter _linePosition;

    public bool IsDroped => _isDroped;

    private void OnValidate()
    {
        _poolObject = FindObjectOfType<PoolObject>();
        _rigidbody = GetComponent<Rigidbody>();
        _pencilAudioPlayer = GetComponent<PencilAudioPlayer>();
    }

    public void RemovePencilInLine()
    {
        _isDroped = true;
        _linePosition.RemovePencile(GetComponent<Pencil>());
    }

    public void TakeLinePositionSetter(LinePositionSetter linePosition)
    {
        _linePosition = linePosition;
    }

    public void FallOverBorder(bool isLeftBorder)
    {
        ChangeParent();
        RemovePencilInLine();
        ChangeStateRigidbody();
        _collider.enabled = false;

        if (isLeftBorder)
        {
            _rigidbody.AddForce(Vector3.left * _forceImpulse, ForceMode.Impulse);
            _rigidbody.AddTorque(Vector3.forward * _multiplicatorTorque, ForceMode.Impulse);
        }
        else
        {
            _rigidbody.AddForce(Vector3.right * _forceImpulse, ForceMode.Impulse);
            _rigidbody.AddTorque(Vector3.back * _multiplicatorTorque, ForceMode.Impulse);
        }
    }

    public void Drop(Transform centrMassBlock, bool _isObjectBreaks)
    {
        if (_linePosition != null)
        {
            ChangeParent(_isObjectBreaks);
            RemovePencilInLine();
            ChangeStateRigidbody();
            float rotateAfterCollision = 10;
            _rigidbody.AddTorque(new Vector3(0, rotateAfterCollision, 0) * _multiplicatorTorque, ForceMode.Impulse);
            gameObject.GetComponent<PencilIncliner>().enabled = false;
            Invoke(nameof(TurnOffPencil), _timeToTurnoff);
        }
    }

    private void ChangeParent(bool isNeedEffects = false)
    {
        if (isNeedEffects == false)
        {
            _pencilEffects.gameObject.SetActive(false);
        }

        transform.parent = _poolObject.transform;
    }

    private void ChangeStateRigidbody()
    {
        _pencilAudioPlayer.PlayDropSound();
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }

    private void TurnOffPencil()
    {
        gameObject.SetActive(false);
    }
}
