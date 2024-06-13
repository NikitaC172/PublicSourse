using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace CraneGame
{
    public class StageText : Stage
    {
        [SerializeField] private List<XRRayInteractor> _rays;
        [SerializeField] private GameObject _canvasInfo;
        [SerializeField] private Button _buttonContinue;
        //public Action ComplitedStage;

        private void OnEnable()
        {
            _buttonContinue.onClick.AddListener(StageComplited);
        }

        private void OnDisable()
        {
            _buttonContinue.onClick.RemoveListener(StageComplited);
        }

        public override void StartStage()
        {
            _canvasInfo.gameObject.SetActive(true);
            SetUpRays(true);
        }

        private void SetUpRays(bool isActive)
        {
            foreach (var ray in _rays)
            {
                    ray.enabled = isActive;
            }
        }

        private void StageComplited()
        {
            _canvasInfo.gameObject.SetActive(false);
            SetUpRays(false);
            ComplitedStage?.Invoke();
        }
    }
}
