using Cinemachine;
using UnityEngine;

public class CinemachineWidthConstant : MonoBehaviour
{
    [SerializeField] private float _resolutionCameraWidht;
    [SerializeField] private float _resolutionCameraHeight;
    [SerializeField] [Range(0f, 1f)] private float _widhtOrHeight = 0.5f;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private float _currentAspectRatio;
    private float _currentFov;
    private float _horizontalFov = 120f;

    private void Start()
    {
        _currentAspectRatio = _resolutionCameraWidht / _resolutionCameraHeight;
        _currentFov = _camera.m_Lens.FieldOfView;
        _horizontalFov = CalculateVerticalFov(_currentFov, 1 / _currentAspectRatio);
    }

    private void LateUpdate()
    {
        if(_currentFov != _camera.m_Lens.FieldOfView)
        {
            float constantWidthFov = CalculateVerticalFov(_horizontalFov, _camera.m_Lens.FieldOfView);
            _camera.m_Lens.FieldOfView = Mathf.Lerp(constantWidthFov, _currentFov, _widhtOrHeight);
        }
    }

    private float CalculateVerticalFov(float horizontalFovInDeg, float aspectRatio)
    {
        float horizontalFovInRads = horizontalFovInDeg * Mathf.Deg2Rad;

        float VerticalFovInRads = 2 * Mathf.Atan(Mathf.Tan(horizontalFovInRads / 2) / aspectRatio);

        return VerticalFovInRads * Mathf.Rad2Deg;
    }
}
