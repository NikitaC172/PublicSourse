using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CraneGame
{
    public class DistanceAssistant : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private GameObject _turnPointBoom;
        [SerializeField] private GameObject _centrTurn;
        [SerializeField] private AssistentCargo _assistentCargo;

        CargoConnectorTrigger _pointCargoJointTrigger;
        private Vector3 _direction;
        private Vector3 _positionTarget;
        private float _distance;
        private float _side;

        public float Distance => _distance;

        private void CalculateDistance()
        {
            _direction = _target.transform.position - _turnPointBoom.transform.position;
            _positionTarget = _target.transform.position - _centrTurn.transform.position;
            _positionTarget.y = 0;
            _direction.y = 0;
            _side = Mathf.Sign(Vector3.Dot(_positionTarget, _direction));//Vector3.Cross(_direction, _turnPointBoom.transform.position)));
            _distance = _direction.magnitude * _side;
        }

        private void OnEnable()
        {
            _assistentCargo.PointUnloadSetted += SetTarget;
            _assistentCargo.PointJointSetted += SetTarget;
        }

        private void OnDisable()
        {
            _assistentCargo.PointUnloadSetted -= SetTarget;
            _assistentCargo.PointJointSetted -= SetTarget;
        }

        private void SetTarget(PointUnload pointUnload)
        {
            if (pointUnload != null)
            {
                _target = pointUnload.GetUnloadUp().gameObject;
                _pointCargoJointTrigger = null;
            }
            else if(_pointCargoJointTrigger == null)
            {
                _target = null;
            }
        }

        private void SetTarget(CargoConnectorTrigger pointCargoJoint)
        {
            if (pointCargoJoint != null)
            {
                _pointCargoJointTrigger = pointCargoJoint;
                _target = _pointCargoJointTrigger.gameObject;
            }
            else
            {
                _target = null;
            }
        }

        private void FixedUpdate()
        {
            if (_target != null)
            {
                CalculateDistance();
            }
        }
    }
}
