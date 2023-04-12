using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaypointCircuit;

public class AIRagDoll : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbodyRoot;
    [SerializeField] private AiBikeController _aiBike;
    [SerializeField] private Rigidbody _rigidbodyRagDollBody;
    [SerializeField] private GameObject _bodyBike;
    [SerializeField] private DistanceCounter _distanceCounter;
    [SerializeField] private AiBikeWaypointCircuit _waypointCircuit;
    [SerializeField] private AiBikeWaypointProgressTracker _waypointProgressTracker;
    [SerializeField] private AudioSource _audioAccidentSource;
    [SerializeField] private float _timeReset = 3.0f;
    [SerializeField] private float _fallSpeedDifference = 5.0f;
    [SerializeField] private string _layerBikeWithCollison = "Bike";
    [SerializeField] private string _layerWithoutCollisionBike = "BikeHidden";

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private int _layerBikeWithCollisonNumber;
    private int _layerWithoutCollisionBikeNumber;

    private bool _isReset = false;

    public float TimeReset => _timeReset;

    private void OnValidate()
    {
        if (_waypointCircuit == null)
        {
            _waypointCircuit = FindObjectOfType<AiBikeWaypointCircuit>();
        }

        _distanceCounter = GetComponent<DistanceCounter>();
        _layerBikeWithCollisonNumber = LayerMask.NameToLayer(_layerBikeWithCollison);
        _layerWithoutCollisionBikeNumber = LayerMask.NameToLayer(_layerWithoutCollisionBike);
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<BarrierTrack>(out BarrierTrack barrier))
        {
            RagDollActivate();
        }
        if (collision.collider.TryGetComponent<AiBikeController>(out AiBikeController aiBike))
        {
            if (collision.relativeVelocity.magnitude > _fallSpeedDifference)
            {
                RagDollActivate();
            }
        }
    }

    public void ChangeTimeReset(float time)
    {
        if (time >= 0)
            _timeReset = time;
    }

    private void RagDollActivate()
    {
        _audioAccidentSource.Play();
        float decelerationAfterCollision = 0.7f;
        _aiBike.enabled = false;
        Vector3 velocity = _rigidbodyRoot.velocity / decelerationAfterCollision;
        _rigidbodyRoot.velocity = Vector3.zero;
        _rigidbodyRagDollBody.velocity = velocity;
        gameObject.layer = _layerWithoutCollisionBikeNumber;
        _bodyBike.SetActive(false);
        _rigidbodyRagDollBody.gameObject.SetActive(true);

        if (_isReset == false)
        {
            _isReset = true;
            Invoke(nameof(ResetRagDoll), _timeReset);
        }
    }

    private void ResetRagDoll()
    {
        _rigidbodyRagDollBody.gameObject.SetActive(false);
        _bodyBike.SetActive(true);
        TakePositionReset();
        gameObject.layer = _layerBikeWithCollisonNumber;
        _aiBike.enabled = true;
        _rigidbodyRagDollBody.gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _isReset = false;
    }

    private void TakePositionReset()
    {
        if (_distanceCounter.IsStart == true)
        {
            float roadHeightAdjustment = 0.4f;
            float roadHeight = 1.5f;
            AiBikeWaypointCircuit.RoutePoint routePoint = _waypointCircuit.GetRoutePoint(_waypointProgressTracker.progressDistance);
            _aiBike.gameObject.transform.position = routePoint.position + new Vector3(0, roadHeight + roadHeightAdjustment, 0);
            _aiBike.gameObject.transform.LookAt(_waypointProgressTracker.target, Vector3.up);
        }
        else
        {
            _aiBike.gameObject.transform.position = _startPosition;
            _aiBike.gameObject.transform.rotation = _startRotation;
        }
    }
}
