using System;
using UnityEngine;

[RequireComponent(typeof(PencilCaseAudioPlayer))]
public class PencilCase : MonoBehaviour
{
    [SerializeField] private LevelSystem _levelSystem;
    [SerializeField] private PencilCaseAudioPlayer _pencilCaseAudioPlayer;

    public Action ShowedCase;
    public Action OpenedCase;

    private void OnValidate()
    {
        _levelSystem = FindObjectOfType<LevelSystem>();
        _pencilCaseAudioPlayer = GetComponent<PencilCaseAudioPlayer>();
    }

    private void OnEnable()
    {
        _levelSystem.Win += ShowCase;
    }

    private void OnDisable()
    {
        _levelSystem.Win -= ShowCase;
    }

    public void OpenCase()
    {
        _pencilCaseAudioPlayer.PlayOpenSound();
        OpenedCase?.Invoke();
    }

    private void ShowCase()
    {
        ShowedCase?.Invoke();
    }
 }
