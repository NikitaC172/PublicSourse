using UnityEngine;

public class PencilAudioPlayer : MonoBehaviour
{
    [SerializeField] private LineSystemAudioPlayer _audioPlayer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _takeSound;
    [SerializeField] private AudioClip _takeSoundFromPencilCase;
    [SerializeField] private AudioClip _paintPictureSound;
    [SerializeField] private AudioClip _dropSound;

    private bool _isDrop = false;

    private void OnValidate()
    {
        _audioPlayer = FindObjectOfType<LineSystemAudioPlayer>();

        if (_audioPlayer != null)
        {
            _audioSource = _audioPlayer.GetAudioSource();
        }
    }

    public void PlayPaintSound()
    {
        float picth = 1.0f;

        if (_audioSource.clip != _paintPictureSound)
        {
            _audioSource.clip = _paintPictureSound;
        }

        _audioSource.pitch = picth;
        _audioSource.Play();
    }

    public void PlayTakeSoundFromPencilCase()
    {
        float picth = 1.0f;

        if (_audioSource.clip != _takeSoundFromPencilCase)
        {
            _audioSource.clip = _takeSoundFromPencilCase;
        }

        _audioSource.pitch = picth;
        _audioSource.Play();
    }

    public void PlayTakeSound()
    {
        float picth = 1.0f;

        if (_audioSource.clip != _takeSound)
        {
            _audioSource.clip = _takeSound;
        }

        _audioSource.pitch = picth;
        _audioSource.Play();
    }

    public void PlayDropSound()
    {
        if (_isDrop == false)
        {
            _isDrop = true;
            _audioSource.PlayOneShot(_dropSound);
        }
    }
}
