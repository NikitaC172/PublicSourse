using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenuScore : MonoBehaviour
{
    [SerializeField] private ScoreStore _scoreStore;
    [SerializeField] private TMP_Text _text;

    private void OnValidate()
    {
        _scoreStore = FindObjectOfType<ScoreStore>();
    }

    private void Start()
    {
        _text.text = _scoreStore.FinalScore.ToString();
    }
}
