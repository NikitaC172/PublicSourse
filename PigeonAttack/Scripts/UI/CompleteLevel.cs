using UnityEngine;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panelComlite;
    [SerializeField] private Button _buttonComplite;
    [SerializeField] private Score _score;
    [SerializeField] private ParticleSystem _confettiBlastRainbow;

    private void OnEnable()
    {
        _player.Won += HidePanel;
        _score.LevelCompleted += ShowPanel;
    }

    private void OnDisable()
    {
        _player.Won -= HidePanel;
        _score.LevelCompleted += ShowPanel;
    }

    private void ShowPanel()
    {
        _confettiBlastRainbow.Play();
        _panelComlite.SetActive(true);
    }

    private void HidePanel()
    {
        _panelComlite.SetActive(false);
    }
}
