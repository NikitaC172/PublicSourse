using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class AssistentCargo : MonoBehaviour
    {
        [SerializeField] private DistanceAssistant _distanceAssistant;
        [SerializeField] private HeightAssistant _heightAssistant;
        [SerializeField] private TurnAssistant _turnAssistant;
        [SerializeField] private StageManager _stageManager;

        private PointUnload _currentPointUnload;
        private CargoConnectorTrigger _cargoConnectorTrigger;
        private Stage _currentStage;
        private IEnumerator _calculateCargoPoint;
        private string _currentDistance;
        private bool _isActive = false;

        public Action<PointUnload> PointUnloadSetted;
        public Action<CargoConnectorTrigger> PointJointSetted;
        public Action<int, string> ActionChanged;

        private void OnEnable()
        {
            _calculateCargoPoint = CalculateCargoPoint();
            _stageManager.StageChanged += SetStage;
            _stageManager.LevelComlited += StopAssistent;
        }

        private void OnDisable()
        {
            _stageManager.StageChanged -= SetStage;
            _stageManager.LevelComlited -= StopAssistent;
        }

        public enum ActionsWithCargo
        {
            Up = 0,
            TurnRight = 1,
            TurnLeft = 2,
            MoveBack = 3,
            MoveForward = 4,
            Down = 5,
            Ready = 6,
        }

        private void SetStage(Stage stage)
        {
            if (_currentStage != null)
            {
                _currentStage.PointUnloadChanged -= SetPointUnload;
                _currentStage.CargoConnectorSetted -= SetCurrentCargoJoint;
            }

            _currentStage = stage;
            _currentStage.CargoConnectorSetted += SetCurrentCargoJoint;
            _currentStage.PointUnloadChanged += SetPointUnload;
        }

        private void SetCurrentCargoJoint(CargoConnectorTrigger cargoConnectorTrigger)
        {
            _cargoConnectorTrigger = cargoConnectorTrigger;
            PointJointSetted?.Invoke(_cargoConnectorTrigger);

            if (_isActive == false)
            {
                StartCoroutine(_calculateCargoPoint);
            }
        }

        private void SetPointUnload(PointUnload pointUnload)
        {
            _currentPointUnload = pointUnload;
            PointUnloadSetted?.Invoke(_currentPointUnload);

            if (_isActive == false)
            {
                StartCoroutine(_calculateCargoPoint);
            }
        }

        private void SetEvent(ActionsWithCargo actionsWithCargo, string distance)
        {
            if (_currentDistance != distance)
            {
                ActionChanged?.Invoke((int)actionsWithCargo, distance);
                _currentDistance = distance;
            }
        }

        private void StopAssistent()
        {
            _isActive = false;
        }

        private IEnumerator CalculateCargoPoint()
        {
            _isActive = true;
            bool onPointPlace = false;
            float distance;

            while (_isActive == true)
            {
                if (_heightAssistant.HeightUp < 0 && onPointPlace == false)
                {
                    distance = MathF.Abs(MathF.Round(_heightAssistant.HeightUp, 2));
                    SetEvent(ActionsWithCargo.Up, distance.ToString());
                }
                else if (_turnAssistant.Turn < -0.5f || _turnAssistant.Turn > 0.5f)
                {
                    onPointPlace = false;
                    distance = MathF.Abs(MathF.Round(_turnAssistant.Turn, 2));

                    if (_turnAssistant.Turn < 0f)
                    {
                        SetEvent(ActionsWithCargo.TurnRight, distance.ToString());
                    }
                    else
                    {
                        SetEvent(ActionsWithCargo.TurnLeft, distance.ToString());
                    }
                }
                else if (_distanceAssistant.Distance < -0.5f || _distanceAssistant.Distance > 0.5f)
                {
                    onPointPlace = false;
                    distance = MathF.Abs(MathF.Round(_distanceAssistant.Distance, 2));

                    if (_distanceAssistant.Distance < 0f)
                    {
                        SetEvent(ActionsWithCargo.MoveBack, distance.ToString());
                    }
                    else
                    {
                        SetEvent(ActionsWithCargo.MoveForward, distance.ToString());
                    }
                }
                else if (_heightAssistant.HeightPlace > 0.15f)
                {
                    onPointPlace = true;
                    distance = MathF.Abs(MathF.Round(_heightAssistant.HeightPlace, 2));
                    SetEvent(ActionsWithCargo.Down, distance.ToString());
                }
                else
                {
                    SetEvent(ActionsWithCargo.Ready, null);
                }

                yield return null;
            }

            _isActive = false;
            SetEvent(ActionsWithCargo.Ready, null);
        }
    }
}
