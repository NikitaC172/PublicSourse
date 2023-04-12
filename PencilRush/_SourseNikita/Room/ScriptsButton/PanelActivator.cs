using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelActivator : MonoBehaviour
{
    [SerializeField] private Animator _buttonGroup;
    [SerializeField] private Button _buttonChangeFrame;
    [SerializeField] private Button _buttonBackFromFrame;
    [SerializeField] private FrameContentCreator _frameContentCreator;
    [SerializeField] private Image _imageInButtonChangeFrame;
    [SerializeField] private Sprite _spriteFrame;
    [SerializeField] private Sprite _spriteBack;

    private bool _isActiveButtonBackImage = false;

    private const string ShowAnim = "Show";
    private const string HideAnim = "Hide";

    public Action HidedHelper;
    public Action ShowedHelper;

    private void OnValidate()
    {
        _frameContentCreator = FindObjectOfType<FrameContentCreator>();
    }

    private void OnEnable()
    {
        _frameContentCreator.CreatedContent += HidePanel;
        _buttonBackFromFrame.onClick.AddListener(ShowPanel);
        _buttonChangeFrame.onClick.AddListener(ChangeButtonImage);
    }

    private void OnDisable()
    {
        _frameContentCreator.CreatedContent -= HidePanel;
        _buttonBackFromFrame.onClick.RemoveListener(ShowPanel);
        _buttonChangeFrame.onClick.RemoveListener(ChangeButtonImage);
    }

    private void ChangeButtonImage()
    {
        if (_isActiveButtonBackImage == false)
        {
            ShowedHelper?.Invoke();
            _isActiveButtonBackImage = true;
            _imageInButtonChangeFrame.sprite = _spriteBack;
        }
        else
        {
            HidedHelper?.Invoke();
            _isActiveButtonBackImage = false;
            _imageInButtonChangeFrame.sprite = _spriteFrame;
        }
    }

    private void ShowPanel()
    {
        _buttonGroup.Play(ShowAnim);
    }

    private void HidePanel()
    {
        _buttonGroup.Play(HideAnim);
        _imageInButtonChangeFrame.sprite = _spriteFrame;
    }
}
