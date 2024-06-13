using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private List<Cargo> _cargos;
        [SerializeField] private List<PointUnload> _pointUnloads;
        [SerializeField] private List<bool> _isCompliteCargo;
        [SerializeField] private bool _isActivateCargoAfterStart = true;
        [SerializeField] private bool _isDeactivateCargoAfterFinishStage = false;
        [SerializeField] private SpringJoint _joint;
        [SerializeField] private List<GameObject> _gameObjectsForActivationAfterFinish;
        [SerializeField] private List<GameObject> _gameObjectsForDeactivationAfterFinish;

        private Cargo _currentCargo = null;
        private PointUnload _currentPointUnload = null;
        private CargoConnectorTrigger _cargoConnectorTrigger = null;
        private Rigidbody _currentRigidbody = null;
        private bool _isActiveFinder = false;
        private bool _isActiveStage = false;

        public Action ComplitedStage;
        public Action CargoPlaced;
        public Action<PointUnload> PointUnloadChanged;
        public Action<CargoConnectorTrigger> CargoConnectorSetted;

        public virtual void StartStage()
        {
            _isActiveStage = true;

            if (_isActivateCargoAfterStart == true)
            {
                foreach (Cargo cargo in _cargos)
                {
                    cargo.gameObject.SetActive(true);
                }
            }

            SetPointJointCargo();
            StartCoroutine(FindCargo());
        }

        private void FinishStage()
        {
            if (_isDeactivateCargoAfterFinishStage == true)
            {
                foreach (Cargo cargo in _cargos)
                {
                    cargo.gameObject.SetActive(false);
                }
            }

            StopCoroutine(FindCargo());
            ComplitedStage?.Invoke();
            _isActiveStage = false;
            DeactivateGameObjectAfterFinish();
            ActivateGameObjectAfterFinish();
            gameObject.SetActive(false);
        }

        private void ActivateGameObjectAfterFinish()
        {
            foreach (var gameObject in _gameObjectsForActivationAfterFinish)
            {
                gameObject.SetActive(true);
            }
        }

        private void DeactivateGameObjectAfterFinish()
        {
            foreach (var gameObject in _gameObjectsForDeactivationAfterFinish)
            {
                gameObject.SetActive(false);
            }
        }

        private void SetPointJointCargo()
        {
            int currentCargo = _isCompliteCargo.FindIndex(x => x == false);
            _cargoConnectorTrigger = _cargos[currentCargo].GetCargoConnectorTrigger();
            CargoConnectorSetted?.Invoke(_cargoConnectorTrigger);
        }

        private void FindPointUnload()
        {
            if (_cargos.Contains(_currentCargo))
            {
                _currentPointUnload = _pointUnloads[_cargos.IndexOf(_currentCargo)];
                _pointUnloads[_cargos.IndexOf(_currentCargo)].TakeRigigdBody(_currentRigidbody);
                SetActivePointUnload(true);
            }
        }

        private void SetActivePointUnload(bool isActive)
        {
            _currentPointUnload.gameObject.SetActive(isActive);

            if (isActive == false)
            {
                _currentCargo = null;
                _currentPointUnload = null;
                _currentRigidbody = null;
            }

            PointUnloadChanged?.Invoke(_currentPointUnload);
        }

        private void CheckDelivery()
        {
            if (_currentPointUnload.IsReady == true)
            {
                _isCompliteCargo[_pointUnloads.IndexOf(_currentPointUnload)] = true;
                CargoPlaced?.Invoke();
                CheckStage();
            }
            else
            {
                CheckStage();
            }
        }

        private void CheckStage()
        {
            if (_isCompliteCargo.Contains(false) == false)
            {
                FinishStage();
            }
            else
            {
                SetPointJointCargo();
            }
        }

        private IEnumerator FindCargo()
        {
            _isActiveFinder = true;

            while (_isActiveFinder == true)
            {
                yield return new WaitWhile(() => _joint.connectedBody == null);
                _currentRigidbody = _joint.connectedBody;
                _currentCargo = _currentRigidbody.GetComponent<Cargo>();
                FindPointUnload();
                yield return new WaitWhile(() => _joint.connectedBody != null);
                CheckDelivery();
                SetActivePointUnload(false);
            }
            yield return null;
        }
    }
}
