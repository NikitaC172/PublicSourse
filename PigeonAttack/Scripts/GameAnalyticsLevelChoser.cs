using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsLevelChoser : MonoBehaviour
{
    [SerializeField] private LevelSelector _selector;

    private void Start()
    {
        GameAnalytics.Initialize();
    }

    private void OnEnable()
    {
        _selector.LevelSelected += TakeAnalytics;
    }

    private void TakeAnalytics(string levelName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, levelName);
    }
}
