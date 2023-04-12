using System.Collections.Generic;
using UnityEngine;

public class FrameSelector : MonoBehaviour
{
    private const string CurrentFrameName = nameof(CurrentFrameName);

    [SerializeField] private FrameStore _picturePointFrameStore;
    [SerializeField] private PictureOpener _pictureOpener;
    [SerializeField] private List<Frame> _frames;
    [SerializeField] private PicturePointAnimator _picturePointAnimator;
    [SerializeField] private MoneySystem _moneySystem;
    [SerializeField] private bool _isNeedAnimator = true;

    private FrameView _frameView;
    private FrameView _activeFrameView;
    private int _viewedFrame;

    public int CurrentFrame { get { return PlayerPrefs.GetInt(CurrentFrameName + gameObject.name, 0); } set { PlayerPrefs.SetInt(CurrentFrameName + gameObject.name, value); } }

    private void OnValidate()
    {
        _moneySystem = FindObjectOfType<MoneySystem>();
    }

    private void Start()
    {
        if (_isNeedAnimator == true)
        {
            if (_pictureOpener.IsOpen == true || _pictureOpener.IsReadyToOpen)
            {
                _frames = _picturePointFrameStore.Frames;
                int numberFrameFromSave = CurrentFrame;
                _viewedFrame = CurrentFrame;
                _frames[numberFrameFromSave].SetActive(true);
                SetNumberFrame(numberFrameFromSave);
            }
        }
        else
        {
            _frames = _picturePointFrameStore.Frames;
            int numberFrameFromSave = CurrentFrame;
            _viewedFrame = CurrentFrame;
            _frames[numberFrameFromSave].SetActive(true);
            SetNumberFrame(numberFrameFromSave);
        }
    }

    public bool TryTakeFrame(Frame frame, int sequenceNumber)
    {
        if (_moneySystem.TryBuy(frame.GetCost()) == true)
        {
            _frames[CurrentFrame].SetActive(false);
            _activeFrameView.ActivateImageChoose(false);
            CurrentFrame = sequenceNumber;
            _frames[CurrentFrame].SetActive(true);
            frame.SetOpen();
            _activeFrameView = _frameView;
            _activeFrameView.ActivateImageChoose(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetCurrentActiveFrameView(FrameView frameView)
    {
        _activeFrameView = frameView;        
    }

    public void SetNumberFrame(int numberFrame)
    {
        TurnOffFrame(CurrentFrame);
        _frames[CurrentFrame].SetActive(false);
        CurrentFrame = numberFrame;
        _frames[CurrentFrame].SetActive(true);
        TurnOnFrame(CurrentFrame);
    }

    public void ResetFrame()
    {
        if (CurrentFrame != _viewedFrame)
        {            
            ChooseFrameBack();
        }
    }

    public void TryChooseFrame(int numberFrame, FrameView frameView)
    {
        if (_frameView != null)
        {
            _frameView.ReturnButton();
        }

        if (_isNeedAnimator == true)
        {
            _picturePointAnimator.ShowAnim();
        }

        if (_frames[numberFrame].GetOpenStatus())
        {
            TurnOffFrame(_viewedFrame);
            _activeFrameView.ActivateImageChoose(false);
            SetNumberFrame(numberFrame);
            _viewedFrame = CurrentFrame;
            _activeFrameView = frameView;
            _activeFrameView.ActivateImageChoose(true);
        }
        else
        {
            TurnOffFrame(_viewedFrame);
            _viewedFrame = numberFrame;
            TurnOnFrame(_viewedFrame);
            _frameView = frameView;
        }
    }

    private void ChooseFrameBack()
    {
        if (_isNeedAnimator == true)
        {
            _picturePointAnimator.ShowAnim();
        }

        TurnOffFrame(_viewedFrame);
        TurnOnFrame(CurrentFrame);
        _viewedFrame = CurrentFrame;

    }

    private void TurnOffFrame(int numberFrame)
    {
        _frames[numberFrame].Deactivation();
    }

    private void TurnOnFrame(int numberFrame)
    {
        _frames[numberFrame].gameObject.SetActive(true);
    }
}
