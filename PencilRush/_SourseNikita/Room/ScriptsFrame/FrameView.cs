using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameView : MonoBehaviour
{
    [SerializeField] private Button _buttonShow;
    [SerializeField] private Button _buttonPanel;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private Button _buttonAds;
    [SerializeField] private TMP_Text _textCost;
    [SerializeField] private Image _imageFrame;
    [SerializeField] private Image _imageFrameActive;
    [SerializeField] private ButtonAds _buttonAdsScript;

    private LevelSystem _levelSystem;
    private FrameSelector _frameSelector;
    private Frame _frame;
    private bool _isOpen;
    private int _sequenceNumber;

    private void OnEnable()
    {
        _buttonShow.onClick.AddListener(ShowFrame);
        _buttonPanel.onClick.AddListener(ShowFrame);
        _buttonBuy.onClick.AddListener(TryTake);
        _buttonAds.onClick.AddListener(TryTake);
    }

    private void OnDisable()
    {
        _buttonShow.onClick.RemoveListener(ShowFrame);
        _buttonPanel.onClick.RemoveListener(ShowFrame);
        _buttonBuy.onClick.RemoveListener(TryTake);
        _buttonAds.onClick.RemoveListener(TryTake);
    }

    public void GetLevelSystem(LevelSystem levelSystem)
    {
        _levelSystem = levelSystem;
    }

    public void CreatePanelFrame(Frame frame, FrameSelector frameSelector, int sequenceNumber)
    {
        _frameSelector = frameSelector;
        _buttonAdsScript.GetLevelSystem(_levelSystem);

        if (frame.GetActiveStatus())
        {
            _frameSelector.GetCurrentActiveFrameView(this);
        }

        _sequenceNumber = sequenceNumber;
        _frame = frame;
        _isOpen = _frame.GetOpenStatus();
        _textCost.text = _frame.GetCost().ToString();
        ActivateImageChoose(frame.GetActiveStatus());

        if (_isOpen == true)
        {
            _imageFrame.sprite = _frame.GetCover();
            TurnOffAllButton();
        }
        else
        {
            _imageFrame.sprite = _frame.GetCoverInactive();
            _buttonShow.gameObject.SetActive(true);
            _buttonBuy.gameObject.SetActive(false);
            _buttonAds.gameObject.SetActive(false);
        }
    }

    public void ActivateImageChoose(bool isActive)
    {
        _imageFrameActive.enabled = isActive;
    }

    public void SetButton()
    {
        if (_frame.GetOpenStatus())
        {
            _imageFrameActive.enabled = true;
            TurnOffAllButton();
        }
        else if (_frame.GetSaleStatus())
        {
            _buttonBuy.gameObject.SetActive(true);
            _buttonShow.gameObject.SetActive(false);
            _buttonAds.gameObject.SetActive(false);
        }
        else
        {
            _buttonAds.gameObject.SetActive(true);
            _buttonBuy.gameObject.SetActive(false);
            _buttonShow.gameObject.SetActive(false);
        }
    }

    public void ReturnButton()
    {
        if (_isOpen == true)
        {
            TurnOffAllButton();
        }
        else
        {
            _buttonShow.gameObject.SetActive(true);
            _buttonAds.gameObject.SetActive(false);
            _buttonBuy.gameObject.SetActive(false);
        }
    }

    private void TryTake()
    {
        if (_frameSelector.TryTakeFrame(_frame, _sequenceNumber) == true)
        {
            _isOpen = true;
            ActivateImageChoose(true);
            _imageFrame.sprite = _frame.GetCover();
            ReturnButton();
        }
    }

    private void TurnOffAllButton()
    {
        _buttonShow.gameObject.SetActive(false);
        _buttonBuy.gameObject.SetActive(false);
        _buttonAds.gameObject.SetActive(false);
    }

    private void ShowFrame()
    {
        _frameSelector.TryChooseFrame(_sequenceNumber, this);
        SetButton();
    }
}
