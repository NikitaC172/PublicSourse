using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationLogIn : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const string EnglishLanguage = "English";
    private const string RussianLanguage = "Russian";
    private const string TurkishLanguage = "Turkish";

    private const string _en = "To get into the leaderboard, you need to log in.";
    private const string _ru = "Для попадания в лидерборд, нужно авторизоваться.";
    private const string _tr = "Skor tablosuna girmek için giriş yapmalısınız.";

    [SerializeField] private YandexInit _yandexInit;

    public bool IsDetected { get; private set; }

    private void OnValidate()
    {
        _yandexInit = FindObjectOfType<YandexInit>();
    }

    private void OnEnable()
    {
        _yandexInit.Completed += OnCompleted;
    }

    private void OnDisable()
    {
        _yandexInit.Completed -= OnCompleted;
    }

    private void OnCompleted()
    {
        string language;

        switch (Agava.YandexGames.YandexGamesSdk.Environment.i18n.lang)
        {
            case EnglishCode:
                language = EnglishLanguage;
                _text.text = _en;
                break;
            case RussianCode:
                language = RussianLanguage;
                _text.text = _ru;
                break;
            case TurkishCode:
                language = TurkishLanguage;
                _text.text = _tr;
                break;
            default:
                language = EnglishLanguage;
                _text.text = _en;
                break;
        }
    }
}
