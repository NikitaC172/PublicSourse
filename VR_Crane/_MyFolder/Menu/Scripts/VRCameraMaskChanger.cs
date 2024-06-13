using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraMaskChanger : MonoBehaviour
{
    [SerializeField] private HideLoadScreen _loadScreen;
    [SerializeField] private Camera _cameraVR;

    private void Awake()
    {
        MaskOff();
    }

    private void Start()
    {
        _loadScreen.Deactivated += MaskOn;
    }

    private void OnDisable()
    {
        _loadScreen.Activated -= MaskOff;
        _loadScreen.Deactivated -= MaskOn;
    }

    private void MaskOff()
    {
        _cameraVR.cullingMask = _cameraVR.cullingMask ^ (1 << 6);
    }

    private void MaskOn()
    {
        _cameraVR.cullingMask = _cameraVR.cullingMask ^ (1 << 6);
    }
}
