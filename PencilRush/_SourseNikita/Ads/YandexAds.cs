using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class YandexAds : MonoBehaviour
{
    [SerializeField] private bool _isNeedAds = false;
    [SerializeField] private GamePause _gamePause;

    public event Action OpenedAd;
    public event Action ReawardedAd;
    public event Action<bool> ClosedAd;
    public event Action ClosedRewardAd;

    private void OnValidate()
    {
        _gamePause = FindObjectOfType<GamePause>();
    }

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private void OnEnable()
    {
        OpenedAd += _gamePause.PauseGame;
        ClosedAd += _gamePause.ContinueGame;
        ClosedRewardAd += _gamePause.ContinueGame;
    }

    private IEnumerator Start()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
        #endif

        yield return YandexGamesSdk.Initialize();

        if (_isNeedAds == true)
        {
            ShowAds();
        }
    }

    private void OnDisable()
    {
        OpenedAd -= _gamePause.PauseGame;
        ClosedAd -= _gamePause.ContinueGame;
        ClosedRewardAd -= _gamePause.ContinueGame;
    }

    public void ShowAds()
    {
        if (Application.isEditor == false)
        {
            InterstitialAd.Show(OpenedAd, ClosedAd);
        }
    }

    public void ShowRewardAds()
    {
        _gamePause.PauseGame();

        if (Application.isEditor == false)
        {
            VideoAd.Show(OpenedAd, default, ClosedRewardAd, default);
        }
    }
}
