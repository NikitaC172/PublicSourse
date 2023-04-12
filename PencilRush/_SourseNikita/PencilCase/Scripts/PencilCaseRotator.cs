using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PencilCase))]
[RequireComponent(typeof(PencilCaseMoverToPoint))]
public class PencilCaseRotator : MonoBehaviour
{
    [SerializeField] private PencilCase _pencilCase;
    [SerializeField] private PencilCaseMoverToPoint _moverToPoint;
    [SerializeField] private PencilCaseObjectModel _pencilCaseObject;
    [SerializeField] private float _fullTurnTime = 3.5f;
    [SerializeField] private float _timeToResetAngle = 0.5f;
    private float deltaTime = 0.01f;
    private bool _isrotate = true;

    private void OnValidate()
    {
        _moverToPoint = GetComponent<PencilCaseMoverToPoint>();
        _pencilCase = GetComponent<PencilCase>();
    }

    private void OnEnable()
    {
        _moverToPoint.Reached += StartRotation;
        _pencilCase.OpenedCase += SetStopRotation;
    }

    private void OnDisable()
    {
        _moverToPoint.Reached -= StartRotation;
        _pencilCase.OpenedCase -= SetStopRotation;
    }

    private void SetStopRotation()
    {
        _isrotate = false;
    }

    private void StartRotation()
    {
        StartCoroutine(Rotation());
    }

    private IEnumerator Rotation()
    {
        float roundeCircle = 360;
        float currentAngle = 0;
        float deltaAngle = (roundeCircle / _fullTurnTime) * deltaTime;
        var waitSeconds = new WaitForSeconds(deltaTime);

        while (_isrotate)
        {
            _pencilCaseObject.transform.localEulerAngles = new Vector3(0, currentAngle, 0);
            currentAngle += deltaAngle;
            yield return waitSeconds;
        }

        float time = 0;
        float needAngle = 360;
        Vector3 currentLocalAngle = _pencilCaseObject.transform.localEulerAngles;
        Vector3 targetAngle = new Vector3(currentLocalAngle.x, needAngle, currentLocalAngle.z);

        while (time < _timeToResetAngle)
        {
            time += Time.deltaTime;
            _pencilCaseObject.transform.localEulerAngles = Vector3.Lerp(currentLocalAngle, targetAngle, time / _timeToResetAngle);
            yield return null;
        }
    }
}
