using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class PointUnloadRotate : MonoBehaviour
    {
        [SerializeField] private Rigidbody _cargoRigidbody;

        private bool _isWork = false;
        private float _delta;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Cargo>(out Cargo cargo))
            {
                _isWork = true;
                _cargoRigidbody = cargo.GetRigidbody();
                StartCoroutine(Rotate());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Cargo>(out Cargo cargo))
            {
                _isWork = false;
            }
        }

        private void CalculateDeltaRot()
        {
            _delta = transform.rotation.y - _cargoRigidbody.transform.rotation.y;
        }

        private IEnumerator Rotate()
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

            _cargoRigidbody = null;
        }
    }
}
