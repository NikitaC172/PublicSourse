using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerQuality : MonoBehaviour
{
    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.Initialize();

        int lowQualityPreset = 0;

        if (Device.Type == Agava.YandexGames.DeviceType.Mobile)
        {
            if(QualitySettings.GetQualityLevel() != lowQualityPreset)
            {
                QualitySettings.SetQualityLevel(lowQualityPreset, true);
            }
        }
    }
}
