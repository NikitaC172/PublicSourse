using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class PositionSetter : MonoBehaviour
    {
        [SerializeField] private PositionVR _position;

        private void Update()
        {
            transform.position = _position.transform.position;
            transform.rotation = _position.transform.rotation;
        }
    }
}
