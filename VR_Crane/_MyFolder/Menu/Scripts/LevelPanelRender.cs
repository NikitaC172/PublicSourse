using DaVanciInk.AdvancedPlayerPrefs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelRender : MonoBehaviour
{
    [SerializeField] private Button _buttonLevel;
    [SerializeField] private List<Image> _stars;
    [SerializeField] private Image _panelStars;
    [SerializeField] private bool _isFistLevel = false;
    [SerializeField] private string _currentLevelinSaveData;
    [SerializeField] private string _prevLevelinSaveData;
    [SerializeField] private Color _colorYellow = Color.yellow;

    private void OnEnable()
    {
        if(_isFistLevel == false)
        {
            if (AdvancedPlayerPrefs.GetInt(_prevLevelinSaveData) == -1)
            {
                HideLevel();
            }
            else
            {
                //SetPanel();
                SetLevel();
            }
        }
        else
        {
            //SetPanel();
            SetLevel();
        }
    }

    private void SetPanel() //???
    {
        Color color = _panelStars.color;
        color.a = 1;
        _panelStars.color = color;
    }

    private void HideLevel()
    {
        _buttonLevel.interactable = false;
    }

    private void SetLevel()
    {
        Color color = _stars[0].color;
        color.a = 1;

        foreach (var star in _stars)
        {
            star.color = color;
        }

        LoadData();
    }

    private void LoadData()
    {
        int starsOpen = AdvancedPlayerPrefs.GetInt(_currentLevelinSaveData);
        Debug.Log(starsOpen);


        if(starsOpen == 0 || starsOpen == -1)
        {
            return;
        }
        else
        {
            for (int i = 0; i <= starsOpen - 1; i++)
            {
                _stars[i].color = _colorYellow;
            }
        }
    }
}
