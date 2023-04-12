using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameContentCreator : MonoBehaviour
{
    [SerializeField] private FrameView _frameViewPrefab;
    [SerializeField] private GameObject _conteiner;
    [SerializeField] private FrameObjectPanel _frameObjectPanel;
    [SerializeField] private Button _buttonBack;
    [SerializeField] private LevelSystem _levelSystem;

    private List<Frame> _frames = new List<Frame>();
    private List<FrameView> _frameViews = new List<FrameView>();
    private FrameSelector _frameSelector;

    public Action CreatedContent;

    private void OnValidate()
    {
        _levelSystem = FindObjectOfType<LevelSystem>();
    }

    public void CreateFrameView(List<Frame> frames, FrameSelector frameSelector)
    {
        RemoveContent();

        foreach (Frame frame in frames)
        {
            _frames.Add(frame);
        }

        for (int i = 0; i < _frames.Count; i++)
        {
            var frameView = Instantiate(_frameViewPrefab, _conteiner.transform) as FrameView;
            frameView.GetLevelSystem(_levelSystem);
            frameView.CreatePanelFrame(_frames[i], frameSelector, i);
            _frameViews.Add(frameView);
        }

        _frameObjectPanel.gameObject.SetActive(true);
        _buttonBack.onClick.AddListener(DeactivationPanel);
        _frameSelector = frameSelector;
        CreatedContent?.Invoke();
    }

    private void RemoveContent()
    {
        if (_frameViews.Count > 0)
        {
            foreach (FrameView frameView in _frameViews)
            {                
                Destroy(frameView.gameObject);
            }

            _frames = new List<Frame>();
            _frameViews = new List<FrameView>();
        }
    }

    private void DeactivationPanel()
    {
        _frameSelector.ResetFrame();
        _frameObjectPanel.HidePanel();
        _buttonBack.onClick.RemoveListener(DeactivationPanel);
    }
}
