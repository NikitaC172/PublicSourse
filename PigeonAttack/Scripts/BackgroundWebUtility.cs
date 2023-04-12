using Agava.WebUtility;
using UnityEngine;

public class BackgroundWebUtility : MonoBehaviour
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        Time.timeScale = inBackground ? 0f : 1f;
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
    }
}
