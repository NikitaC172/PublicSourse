using CraneGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class HeightAssistant : MonoBehaviour
    {
        [SerializeField] private GameObject _targetUp;
        [SerializeField] private GameObject _targetPlace;
        [SerializeField] private CargoJoint _cargoJoint;
        [SerializeField] private AssistentCargo _assistentCargo;

        CargoConnectorTrigger _pointCargoJointTrigger;
        private GameObject _pointForMove;
        private Cargo _currentCargoPlacePoint;
        private float _heightUp;
        private float _heightPlace;

        public float HeightUp => _heightUp;
        public float HeightPlace => _heightPlace;

        private void CalculateHeight()
        {
            _heightUp = _pointForMove.transform.position.y - _targetUp.transform.position.y;
            _heightPlace = _pointForMove.transform.position.y - _targetPlace.transform.position.y;
        }

        private void OnEnable()
        {
            _assistentCargo.PointUnloadSetted += SetTargetCargoPlace;
            _assistentCargo.PointJointSetted += SetTargetCargoJoint;
        }

        private void OnDisable()
        {
            _assistentCargo.PointUnloadSetted -= SetTargetCargoPlace;
            _assistentCargo.PointJointSetted -= SetTargetCargoJoint;
        }

        private void SetTargetCargoPlace(PointUnload pointUnload)
        {
            if (pointUnload != null)
            {
                _currentCargoPlacePoint = _cargoJoint.Cargo.GetComponent<Cargo>().GetCargoPointPlace();
                _pointForMove = _currentCargoPlacePoint.gameObject;
                _targetUp = pointUnload.GetUnloadUp().gameObject;
                _targetPlace = pointUnload.GetUnloadPlace().gameObject;
                _pointCargoJointTrigger = null;
            }
            else if(_pointCargoJointTrigger == null)
            {
                _targetUp = null;
                _targetPlace = null;
                _currentCargoPlacePoint = null;
            }
        }

        private void SetTargetCargoJoint(CargoConnectorTrigger cargoConnectorTrigger)
        {
            if (cargoConnectorTrigger != null)
            {
                _pointCargoJointTrigger = cargoConnectorTrigger;
                _pointForMove = _cargoJoint.gameObject;
                _targetUp = _pointCargoJointTrigger.gameObject;
                _targetPlace = _pointCargoJointTrigger.gameObject;
            }
            else
            {
                _targetUp = null;
                _targetPlace = null;
                _pointCargoJointTrigger = null;
            }
        }

        private void FixedUpdate()
        {
            if (_targetUp != null && _targetPlace != null)
            {
                CalculateHeight();
            }
        }
    }
}
