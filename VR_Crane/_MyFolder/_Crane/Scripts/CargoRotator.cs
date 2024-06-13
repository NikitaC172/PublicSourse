using CraneGame;
using System.Collections;
using UnityEngine;

public class CargoRotator : MonoBehaviour
{
    [SerializeField] private SimpleAxisRotator _craneRotator;

    private Rigidbody _cargoRigidbody;
    private float _zAxisCraneNewRot;
    private float _zAxisCraneCurrentRot;
    private float _delta;
    private bool _isWork = false;

    public void Activate(Rigidbody rigidbody)
    {
        _cargoRigidbody = rigidbody;
        _isWork = true;
        _zAxisCraneCurrentRot = _craneRotator.transform.localRotation.z;
        StartCoroutine(CargoRotatorEnumerator());
    }

    public void Deactivate()
    {
        _isWork = false;
    }

    private void SetDirectionCargo(Quaternion quaternion)
    {

    }

    private void CalculateDeltaRot()
    {
        _zAxisCraneNewRot = _craneRotator.transform.localRotation.z;
        _delta = _zAxisCraneNewRot - _zAxisCraneCurrentRot;
        _zAxisCraneCurrentRot = _zAxisCraneNewRot;
    }

    private IEnumerator CargoRotatorEnumerator()
    {
        float timeSmooth = 0.02f;
        float timeMove = 1.0f;

        while (_isWork == true)
        {            
            CalculateDeltaRot();
            float time = 0f;

            while (_isWork == true && time < timeMove)
            {
                time += timeSmooth;
                _cargoRigidbody.transform.rotation = new Quaternion(
                    _cargoRigidbody.transform.rotation.x,
                    _cargoRigidbody.transform.rotation.y + _delta / (timeMove / timeSmooth),
                    _cargoRigidbody.transform.rotation.z,
                    _cargoRigidbody.transform.rotation.w);
                yield return new WaitForSeconds(timeSmooth);
            }
            yield return null;
        }
    }
}
