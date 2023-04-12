using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameStore : MonoBehaviour
{
    [SerializeField] private List<Frame> _frames;
    [SerializeField] private FrameContentCreator _frameContentCreator;
    [SerializeField] private FrameSelector _frameSelector;
    [SerializeField] private CinemachineVirtualCamera _cameraPicture;
    [SerializeField] private Button _buttonBack;

    public List<Frame> Frames => _frames;

    private void OnValidate()
    {
        _frameContentCreator = FindObjectOfType<FrameContentCreator>();
    }

    private void OnEnable()
    {
        _buttonBack.onClick.AddListener(DeactivationCamera);
    }

    private void OnDisable()
    {
        _buttonBack.onClick.RemoveListener(DeactivationCamera);
    }

    public void ShowFrames()
    {
        _cameraPicture.Priority = 1;
        _frameContentCreator.CreateFrameView(_frames, _frameSelector);
    }

    private void DeactivationCamera()
    {
        _cameraPicture.Priority = 0;
    }
}
