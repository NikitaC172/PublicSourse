using Cinemachine;
using UnityEngine;

public class CinemachineChangerFov : MonoBehaviour
{
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private float _resolutionCameraWidht = 2385;
    [SerializeField] private float _resolutionCameraHeight = 1323;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private float aspectRatioBase;
    private float currentAspectRatio;

    private const float AdjustmentPov = 24;
    private const float DefaultFovCamera = 40;

    private void Start()
    {
        aspectRatioBase = _resolutionCameraWidht / _resolutionCameraHeight;        
        currentAspectRatio = aspectRatioBase;
    }

    private void Update()
    {
        if (currentAspectRatio != _cameraMain.aspect)
        {
            currentAspectRatio = _cameraMain.aspect;

            if (currentAspectRatio < aspectRatioBase)
            {
                Debug.Log(_camera.m_Lens.Aspect);
                float differenceBetweenFov = aspectRatioBase - currentAspectRatio;
                _camera.m_Lens.FieldOfView = DefaultFovCamera + differenceBetweenFov * AdjustmentPov;
            }
            else
            {
                _camera.m_Lens.FieldOfView = DefaultFovCamera;
            }
        }
    }
}
