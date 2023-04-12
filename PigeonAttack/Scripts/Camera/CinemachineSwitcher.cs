using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameCamera;
    [SerializeField] private CinemachineVirtualCamera _dieCamera;
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private Player _player;
    [SerializeField] private Scene _scene;

    private void OnEnable()
    {
        _player.Dead += SwitchCameraZoomIn;
        _player.Won += SwitchCameraZoomIn;
        _scene.Restarted += SwitchCameraZoomOut;
    }

    private void OnDisable()
    {
        _player.Dead -= SwitchCameraZoomIn;
        _player.Won -= SwitchCameraZoomIn;
        _scene.Restarted -= SwitchCameraZoomOut;
    }

    private void SwitchCameraZoomOut()
    {
        float timeRestartCamera = 0.1f;
        _cinemachineBrain.m_DefaultBlend.m_Time = timeRestartCamera;
        _dieCamera.Priority = 0;
        _gameCamera.Priority = 1;
    }

    private void SwitchCameraZoomIn()
    {
        float timeZoomCamera = 2.0f;
        _cinemachineBrain.m_DefaultBlend.m_Time = timeZoomCamera;
        _gameCamera.Priority = 0;
        _dieCamera.Priority = 1;
    }

}
