using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace CraneGame
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private List<Stage> _stages;
        [SerializeField] private GameObject _canvasFinishGame;
        [SerializeField] private GameObject _canvasFailGame;
        [SerializeField] private AngleCalculator _angleCalculator;
        [SerializeField] private List<XRRayInteractor> _rays;
        [SerializeField] private HandAController _handAController;

        private Stage _currentStage;
        private int _currentStageIndex = 0;

        public Action<Stage> StageChanged;
        public Action LevelComlited;

        private void Start()
        {
            ActivateStage(_currentStageIndex);
        }

        private void OnEnable()
        {
            _angleCalculator.GameOver += GameFailed;
        }

        private void OnDisable()
        {
            _angleCalculator.GameOver -= GameFailed;
        }

        private void ActivateStage(int indexStage)
        {
            _currentStage = _stages[indexStage];
            StageChanged?.Invoke(_currentStage);
            _currentStage.StartStage();
            _currentStage.ComplitedStage += NextStage;
        }

        private void NextStage()
        {
            if(_currentStageIndex < _stages.Count - 1)
            {
                _currentStage.ComplitedStage -= NextStage;
                _currentStageIndex++;
                ActivateStage(_currentStageIndex);
            }
            else
            {
                LevelComlited?.Invoke();
                GameFinish();
                //Debug.LogWarning("FINISH GAME");
            }
        }

        public void GameFailed()
        {
            SetUpRays(true);
            _canvasFailGame.SetActive(true);
            _handAController.DisavbleControl();
        }

        public void GameFinish()
        {
            SetUpRays(true);
            _canvasFinishGame.SetActive(true);
            _handAController.DisavbleControl();
        }

        private void SetUpRays(bool isActive)
        {
            foreach (var ray in _rays)
            {
                ray.enabled = isActive;
            }
        }
    }
}
