using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class PointUnloadPlace : MonoBehaviour
    {
        [SerializeField] private Camera _targetVR;
        private bool _isReady = false;

        public Action<Rigidbody> Placed;
        public Action<Rigidbody> UnPlaced;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Cargo>(out Cargo cargo))
            {
                Placed?.Invoke(cargo.GetRigidbody());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Cargo>(out Cargo cargo))
            {
                UnPlaced?.Invoke(cargo.GetRigidbody());
            }
        }

        private void OnEnable()
        {
            StartCoroutine(LookAtCamera());
        }

        private void OnDisable()
        {
            StopCoroutine(LookAtCamera());
        }

        private IEnumerator LookAtCamera()
        {
            Vector3 direction;
            float timeWaitToUpdate = 1f;
            var waitSecond = new WaitForSeconds(timeWaitToUpdate);

            while (true)
            {
                direction = _targetVR.transform.position - transform.position;
                direction.y = 0f;
                transform.rotation = Quaternion.LookRotation(direction);
                yield return waitSecond;
            }
        }
    }
}
