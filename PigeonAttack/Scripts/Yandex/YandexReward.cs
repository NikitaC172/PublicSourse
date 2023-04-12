using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System;

public class YandexReward : MonoBehaviour
{
    [SerializeField] private SoundControl _soundControl;

    public event Action OpenedAd;
    public event Action<bool> ClosedAd;
    public event Action<float> CustomizedPlatform;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private void OnEnable()
    {
        OpenedAd += PauseGame;
        ClosedAd += ContinueGame;
    }

    private IEnumerator Start()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
        #endif

        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.Initialize();

        if (Device.Type == Agava.YandexGames.DeviceType.Mobile)
        {
            float deceleration = 0.8f;
            CustomizedPlatform?.Invoke(deceleration);
        }

        //ShowAds();
        Invoke(nameof(ShowAds), 0.2f);
    }

    private void ShowAds()
    {
        InterstitialAd.Show(OpenedAd, ClosedAd);
    }

    private void OnDisable()
    {
        OpenedAd -= PauseGame;
        ClosedAd -= ContinueGame;
    }

    private void PauseGame()
    {
        _soundControl.SetOffSound();
        Time.timeScale = 0;
    }

    private void ContinueGame(bool isClose)
    {
        Time.timeScale = 1;
        _soundControl.SetOnSound();
    }
}
