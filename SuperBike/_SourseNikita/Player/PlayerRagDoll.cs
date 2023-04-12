using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagDoll : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbodyRoot;
    [SerializeField] private ArcadeBikeController _bikeController;
    [SerializeField] private Rigidbody _rigidbodyRagDollBody;
    [SerializeField] private GameObject _bodyBike;
    [SerializeField] private AiBikeWaypointCircuit _waypointCircuit;
    [SerializeField] private AiBikeWaypointProgressTracker _waypointProgressTracker;
    [SerializeField] private DistanceCounter _distanceCounter;
    [SerializeField] private AudioSource _audioAccidentSource;
    [SerializeField] private AudioSource _audioEngineSource;
    [SerializeField] private float _timeReset = 3.0f;
    [SerializeField] private float _fallSpeedDifference = 5.0f;
    [SerializeField] private string _layerBikeWithCollison = "Bike";
    [SerializeField] private string _layerWithoutCollisionBike = "BikeHidden";

    private Vector3 _startPosition;
    private Vector3 _nextPoint;
    private Quaternion _startRotation;
    private int _layerBikeWithCollisonNumber;
    private int _layerWithoutCollisionBikeNumber;
    private bool _isReset = false;

    private void OnValidate()
    {
        _layerBikeWithCollisonNumber = LayerMask.NameToLayer(_layerBikeWithCollison);
        _layerWithoutCollisionBikeNumber = LayerMask.NameToLayer(_layerWithoutCollisionBike);
        _distanceCounter = GetComponent<DistanceCounter>();
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

    public void ResetRagDoll()
    {
        if (_isReset == true)
        {
            _rigidbodyRagDollBody.gameObject.SetActive(false);
            _rigidbodyRoot.isKinematic = false;
            _bodyBike.SetActive(true);
            TakePositionReset();
            _bikeController.enabled = true;
            gameObject.layer = _layerBikeWithCollisonNumber;
            _rigidbodyRagDollBody.gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _audioEngineSource.mute = false;
            _isReset = false;
        }
    }

    private void RagDollActivate()
    {
        _nextPoint = _waypointProgressTracker.target.position;
        _audioAccidentSource.Play();
        _audioEngineSource.mute = true;
        float decelerationAfterCollision = 0.7f;
        _bikeController.enabled = false;
        Vector3 velocity = _rigidbodyRoot.velocity / decelerationAfterCollision;
        _rigidbodyRoot.isKinematic = true;
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

    private void TakePositionReset()
    {
        if (_distanceCounter.IsStart == true)
        {
            float roadHeightAdjustment = 0.4f;
            float roadHeight = 1.5f;
            AiBikeWaypointCircuit.RoutePoint routePoint = _waypointCircuit.GetRoutePoint(_waypointProgressTracker.progressDistance);
            _bikeController.gameObject.transform.position = routePoint.position + new Vector3(0, roadHeight + roadHeightAdjustment, 0);
            _bikeController.gameObject.transform.LookAt(_nextPoint, Vector3.up);
        }
        else
        {
            _bikeController.gameObject.transform.position = _startPosition;
            _bikeController.gameObject.transform.rotation = _startRotation;
        }
    }
}
