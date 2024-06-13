using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CraneGame
{
    public class TestClick : MonoBehaviour
    {
        [SerializeField] private ArmKeyboardController _keyboard;
        [SerializeField] private Button btnClick;

        private void OnEnable()
        {
            _keyboard.ClickedEnter += Click;
        }

        private void OnDisable()
        {
            _keyboard.ClickedEnter -= Click;
        }

        private void Click()
        {
            btnClick.onClick.Invoke();
        }
    }
}
