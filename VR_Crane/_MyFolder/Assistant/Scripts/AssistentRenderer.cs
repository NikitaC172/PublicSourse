using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace CraneGame
{
    public class AssistentRenderer : MonoBehaviour
    {
        [SerializeField] private AssistentCargo _assistentCargo;
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _textMeshPro;

        private int _currentAction = 0;

        public Action ActionWithCargoChanged;


        private void Awake()
        {
            _assistentCargo.ActionChanged += SetImageAndDistance;
        }

        private void OnDisable()
        {
            _assistentCargo.ActionChanged -= SetImageAndDistance;
        }

        private void SetImageAndDistance(int actionsWithCargo, string distance)
        {
            if(_currentAction != actionsWithCargo)
            {
                ActionWithCargoChanged?.Invoke();
            }

            _currentAction = actionsWithCargo;
            _image.sprite = _sprites[actionsWithCargo];
            _textMeshPro.text = distance;
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
    }
}
