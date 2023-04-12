using Cinemachine;
using UnityEngine;

public class RoomCameraSetter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraRoom;

    public void ActivateCamera()
    {
        _cameraRoom.Priority = 1;
    }

    public void DeactivationCamera()
    {
        _cameraRoom.Priority = 0;
    }
}
