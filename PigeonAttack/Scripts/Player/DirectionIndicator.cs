using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] private PlayerMover _joystickPlayer;
    [SerializeField] private GameObject _arrow;

    private float _offset = 0.5f;

    private void Update()
    {
        float directionForce = Mathf.Sqrt(Mathf.Pow(_joystickPlayer.Direction.x, 2) + Mathf.Pow(_joystickPlayer.Direction.z, 2));
        float directionAngle = Mathf.Atan2(_joystickPlayer.Direction.x, _joystickPlayer.Direction.z) / Mathf.Rad2Deg;
        _arrow.transform.localPosition = new Vector3(0, 0, directionForce + _offset);
        gameObject.transform.localEulerAngles = new Vector3(0, directionAngle, 0);
    }
}
