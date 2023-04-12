using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VKADS : MonoBehaviour
{
    [SerializeField] private SoundControl _soundControl;
    [SerializeField] private VkBridgeController _vkBridge;

    private void OnValidate()
    {
        _vkBridge = FindObjectOfType<VkBridgeController>();
    }

    private void Start()
    {
#if !UNITY_EDITOR
        ShowAds();
#endif
    }

    private void ShowAds()
    {
        PauseGame();
        _vkBridge.VKWebAppShowNativeAds(new VKWebAppShowNativeAdsStruct
        {
            ad_format = AdFormat.reward
        }, AdsResult);
    }

    public void AdsResult(VKWebAppShowNativeAdsResultStruct result)
    {
        //� ���� ����� ������� ��������� ��������� �������
        var adsIsShow = result.result;
        ContinueGame(true);
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
