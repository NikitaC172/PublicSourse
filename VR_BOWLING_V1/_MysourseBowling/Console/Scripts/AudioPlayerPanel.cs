using UnityEngine;

public class AudioPlayerPanel : MonoBehaviour
{
    [SerializeField] private ConsoleButton _consoleButtonBall;
    [SerializeField] private ConsoleButton _consoleButtonPins;
    [SerializeField] private ConsoleStarGame _consoleStartGame;
    [SerializeField] private FinishGame _finishGame;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clipClickPanel;
    [SerializeField] private AudioClip _clipClickButton;
    [SerializeField] private AudioClip _clipClickStartGame;

    private void OnEnable()
    {
        _consoleButtonBall.Activated += PlayOnClickBigButton;
        _consoleButtonPins.Activated += PlayOnClickBigButton;
        _consoleStartGame.Clicked += PlayOnClickPanel;
        _consoleStartGame.StartedGame += PlayOnClickStartGame;
        _finishGame.RestartedGame += PlayOnClickPanel;
    }

    private void OnDisable()
    {
        _consoleButtonBall.Activated -= PlayOnClickBigButton;
        _consoleButtonPins.Activated -= PlayOnClickBigButton;
        _consoleStartGame.Clicked -= PlayOnClickPanel;
        _consoleStartGame.StartedGame -= PlayOnClickStartGame;
        _finishGame.RestartedGame -= PlayOnClickPanel;
    }

    private void PlayOnClickPanel()
    {
        PlaySound(_clipClickPanel);
    }

    private void PlayOnClickBigButton()
    {
        PlaySound(_clipClickButton);
    }

    private void PlayOnClickStartGame(int count)
    {
        PlaySound(_clipClickStartGame);
    }

    private void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
