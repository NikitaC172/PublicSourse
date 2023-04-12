using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelector : MonoBehaviour
{
    [SerializeField] private List<RoomCameraSetter> _roomCameraSetters;
    [SerializeField] private int _openPictures;
    [SerializeField] private int _maxPictureInRoom = 4;
    [SerializeField] private Button _backRoomButton;
    [SerializeField] private Button _nextRoomButton;
    private int _lastOpenedRoom;
    private int _currentRoom = 0;
    private bool _isWork = false;


    private void TakeSaveData()
    {
        _openPictures = SaveSystem.CurrentUnlockInteractivePicture;
    }

    private void Awake()
    {
        TakeSaveData();
        SpotLastOpenRoom();
        SetCamera(_lastOpenedRoom);
    }

    private void OnEnable()
    {
        _backRoomButton.onClick.AddListener(OnClickBackButton);
        _nextRoomButton.onClick.AddListener(OnClickNextButton);
    }

    private void OnDisable()
    {
        _backRoomButton.onClick.RemoveListener(OnClickBackButton);
        _nextRoomButton.onClick.RemoveListener(OnClickNextButton);
    }

    private void OnClickNextButton()
    {
        int newRoom = _currentRoom + 1;

        if (newRoom < _roomCameraSetters.Count)
        {
            SetCamera(newRoom);
        }
    }

    private void OnClickBackButton()
    {
        int newRoom = _currentRoom - 1;

        if (newRoom >= 0)
        {
            SetCamera(newRoom);
        }
    }

    private void SpotLastOpenRoom()
    {
        _lastOpenedRoom = (_openPictures / _maxPictureInRoom);

        if (_lastOpenedRoom < _roomCameraSetters.Count)
        {
            if (_openPictures % _maxPictureInRoom == 0)
            {
                if (_lastOpenedRoom > 0)
                {

                    _lastOpenedRoom--;                    
                }
            }
        }
        else
        {
            _lastOpenedRoom = _roomCameraSetters.Count - 1;
        }
    }

    private void TrySetVisibleButton()
    {
        if (_currentRoom == 0)
        {
            _backRoomButton.gameObject.SetActive(false);
        }
        else
        {
            _backRoomButton.gameObject.SetActive(true);
        }

        if (_currentRoom == _lastOpenedRoom)
        {
            _nextRoomButton.gameObject.SetActive(false);
        }
        else
        {
            _nextRoomButton.gameObject.SetActive(true);
        }
    }

    private void SetCamera(int numberRoom)
    {
        if (_isWork == false)
        {
            _isWork = true;
            _roomCameraSetters[_currentRoom].DeactivationCamera();
            _currentRoom = numberRoom;
            _roomCameraSetters[_currentRoom].ActivateCamera();
            TrySetVisibleButton();
            _isWork = false;
        }
    }
}
