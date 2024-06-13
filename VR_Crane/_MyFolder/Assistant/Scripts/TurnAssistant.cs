using CraneGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class TurnAssistant : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private GameObject _turnPointBoom;
        [SerializeField] private AssistentCargo _assistentCargo;

        CargoConnectorTrigger _pointCargoJointTrigger;
        private Vector3 _direction;
        private Vector3 _base;
        private float _turn;
        private float _turnSide = 0;

        public float Turn => _turn;

        private void CalculateTurn()
        {
            _direction = _target.transform.position - _turnPointBoom.transform.position;
            _base = _turnPointBoom.transform.forward;
            _base.y = 0;
            _direction.y = 0;
            _turnSide = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(_direction, _base)));
            _turn = Vector3.Angle(_direction, _base) * _turnSide;
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
                CalculateTurn();
            }
        }
    }
}
