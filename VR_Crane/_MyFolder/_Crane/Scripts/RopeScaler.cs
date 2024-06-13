using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class RopeScaler : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _scaleFactor;
        private float _distance;

        public void TakeTarget(Transform newTraget)
        {
            _target = newTraget;
        }

        private void FixedUpdate()
        {
            transform.LookAt(_target);
            _distance = (_target.position - transform.position).magnitude;
            transform.localScale = new Vector3(1, 1, _distance * _scaleFactor);
        }
    }
}
