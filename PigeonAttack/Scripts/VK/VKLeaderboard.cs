using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VKLeaderboard : MonoBehaviour
{
    [SerializeField] private VkBridgeController _vkBridge;
    [SerializeField] private ScoreStore _scoreStore;

    private void OnValidate()
    {
        _vkBridge = FindObjectOfType<VkBridgeController>();
        _scoreStore = FindObjectOfType<ScoreStore>();
    }

    public void Invate()
    {
#if !UNITY_EDITOR
        _vkBridge.VKWebAppShowInviteBox();
#endif
    }

    public void ShowLeaderboard()
    {
#if !UNITY_EDITOR
        _vkBridge.VKWebAppShowLeaderBoardBox(_scoreStore.FinalScore);
#endif
    }
}
