using UnityEngine;

public class AudioScoreChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _sourceForScore;
    [SerializeField] private AudioClip _clipKills;
    [SerializeField] private AudioClip _clipBestKills;
    [SerializeField] private AudioClip _clipLevelComplete;
    [SerializeField] private AudioClip _clipColecteBread;
    [SerializeField] private Score _score;
    [SerializeField] private FinalScore _finalScore;

    private bool isStarted = false;

    private void OnEnable()
    {
        _score.ChangedScore += PlayKillsClip;
        _score.ChangedBestScore += PlayBestKillsClip;
        _score.LevelCompleted += PlayCompleteClip;
        _score.CollectedBread += PlayCollecteBreadClip;
        _finalScore.ShowedBread += PlayCollecteBreadClip;
    }

    private void OnDisable()
    {
        _score.ChangedScore -= PlayKillsClip;
        _score.ChangedBestScore -= PlayBestKillsClip;
        _score.LevelCompleted -= PlayCompleteClip;
        _score.CollectedBread -= PlayCollecteBreadClip;
        _finalScore.ShowedBread -= PlayCollecteBreadClip;
    }

    private void PlayCollecteBreadClip()
    {
        _sourceForScore.PlayOneShot(_clipColecteBread);
    }

    private void PlayCompleteClip()
    {
        _sourceForScore.PlayOneShot(_clipLevelComplete);
    }

    private void PlayKillsClip()
    {
        _sourceForScore.PlayOneShot(_clipKills);
    }

    private void PlayBestKillsClip()
    {
        if (isStarted == true)
        {
            _sourceForScore.PlayOneShot(_clipBestKills);
        }

        isStarted = true;
    }
}
