using System;
using UnityEngine;
using UnityEngine.UI;

namespace CraneGame
{
    public class CameraChangeButton : MonoBehaviour
    {
        [SerializeField] private Button _buttonChangeCamera;
        public Action ButtonClick;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TriggerFinger>(out _))
            {
                _buttonChangeCamera.Select();
                ClickButton();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<TriggerFinger>(out _))
            {
                _buttonChangeCamera.interactable = false;
                _buttonChangeCamera.interactable = true;
            }
        }

        private void ClickButton()
        {
            ButtonClick?.Invoke();
        }
    }
}
