using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private List<Camera> _cameras;
        [SerializeField] private CameraChangeButton _changeButton;

        private int _currentCamera = 0;

        private void OnEnable()
        {
            _changeButton.ButtonClick += ChangeCamera;
        }

        private void OnDisable()
        {
            _changeButton.ButtonClick -= ChangeCamera;
        }

        private void ChangeCamera()
        {
            _cameras[_currentCamera].enabled = false;

            if (_currentCamera + 1 >= _cameras.Count)
            {
                _currentCamera = 0;
            }
            else
            {
                _currentCamera++;
            }

            _cameras[_currentCamera].enabled = true;
        }
    }
}
