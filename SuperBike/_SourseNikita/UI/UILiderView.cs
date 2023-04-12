using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class UILiderView : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private PositionChecker _positionChecker;
    [SerializeField] private DistanceCounter _distanceCounterRiders;
    [SerializeField] private TimeCounter _timeCounterRacer;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _positionTextField;
    [SerializeField] private TMP_Text _nameTextField;
    [SerializeField] private float _timeChangePosition = 0.4f;
    [SerializeField] private float _timeShowHide = 0.4f;

    private float _UIfirstpositionY = -30;
    private float _UIStepBetweenPositionY = -60;
    private float _UILastpositionY = -210;
    private float _alfpaGroup;

    private int _currentPosition = -1;
    private int _newPosition;
    private int _visiblePosition = 3;

    public int CurrentPosition => _currentPosition;
    public string Name => _nameTextField.text;
    public TimeCounter TimeCounterRacer => _timeCounterRacer;
    public DistanceCounter DistanceCounterRiders => _distanceCounterRiders;

    private void OnValidate()
    {
        _positionChecker = FindObjectOfType<PositionChecker>();
    }

    private void OnEnable()
    {
        _positionChecker.UpdatedPositions += ChangePosition;
        YandexGame.GetDataEvent += GetLoad;
    }

    private void Start()
    {
        if (_name != "Player")
        {
            _nameTextField.text = _name;
        }
        else
        {
            if (YandexGame.SDKEnabled)
            {
                _nameTextField.text = YandexGame.savesData.Name;
            }
        }
    }

    private void GetLoad()
    {
        if (_name == "Player")
        {            
            _nameTextField.text = YandexGame.savesData.Name;
        }
    }

    private void OnDisable()
    {
        _positionChecker.UpdatedPositions += ChangePosition;
        YandexGame.GetDataEvent -= GetLoad;
    }

    private bool CheckChangePosition(int position)
    {
        if (position == _currentPosition)
        {
            return false;
        }
        else
        {
            _currentPosition = position;
            return true;
        }
    }

    private bool CheckVisibleView(int newPosition)
    {
        if (newPosition >= _visiblePosition && _currentPosition < _visiblePosition)
        {
            _alfpaGroup = 0;
            return true;
        }
        else if (newPosition < _visiblePosition && _currentPosition >= _visiblePosition)
        {
            _alfpaGroup = 1;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangePosition(IReadOnlyList<DistanceCounter> distanceCounters)
    {
        _newPosition = distanceCounters.ToList().IndexOf(_distanceCounterRiders);

        if (CheckVisibleView(_newPosition) == true)
        {
            StartCoroutine(ChangeVisisbleViewPosition());
        }

        if (CheckChangePosition(_newPosition) == true)
        {
            StartCoroutine(ChangeLiderViewPosition());
        }
    }

    private IEnumerator ChangeVisisbleViewPosition()
    {
        float time = 0;


        while (time < _timeShowHide)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, _alfpaGroup, time / _timeShowHide);
            yield return null;
        }
    }

    private IEnumerator ChangeLiderViewPosition()
    {
        float time = 0;
        _positionTextField.text = (_currentPosition + 1).ToString();
        Vector2 _newPosition;

        if (_currentPosition >= 3)
        {
            _newPosition = new Vector2(_rectTransform.anchoredPosition.x, _UILastpositionY);
        }
        else
        {
            _newPosition = new Vector2(_rectTransform.anchoredPosition.x, (_UIfirstpositionY + _UIStepBetweenPositionY * _currentPosition));
        }

        while (time < _timeChangePosition)
        {
            time += Time.deltaTime;
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _newPosition, time / _timeChangePosition);
            yield return null;
        }
    }
}
