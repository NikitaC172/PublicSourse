using System;
using UnityEngine;
using UnityEngine.UI;

public class RoomImageHelper : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;
    [SerializeField] private PanelActivator _panelActivator;
    [SerializeField] private FrameContentCreator _frameContentCreator;

    public Action ShowedHelper;
    public Action HidedHelper;

    private void OnValidate()
    {
        _frameContentCreator = FindObjectOfType<FrameContentCreator>();
    }

    private void OnEnable()
    {
        _panelActivator.HidedHelper += HideHelper;
        _panelActivator.ShowedHelper += ShowHelper;
        _frameContentCreator.CreatedContent += ChangeFrameView;
        _buttonBack.onClick.AddListener(OnClickButtonHide);
    }

    private void OnDisable()
    {
        _panelActivator.HidedHelper -= HideHelper;
        _panelActivator.ShowedHelper -= ShowHelper;
        _frameContentCreator.CreatedContent -= ChangeFrameView;
        _buttonBack.onClick.RemoveListener(OnClickButtonHide);
    }

    private void ShowHelper()
    {
        ShowedHelper?.Invoke();
        _buttonBack.gameObject.SetActive(true);
    }

    private void OnClickButtonHide()
    {
        ShowBackButton(false);
        HideHelper();
    }

    private void ChangeFrameView()
    {
        ShowBackButton(true);
        HideHelper();
    }

    private void ShowBackButton(bool isShow)
    {
        _buttonBack.gameObject.SetActive(isShow);
    }

    private void HideHelper()
    {
        HidedHelper?.Invoke();
    }
}
