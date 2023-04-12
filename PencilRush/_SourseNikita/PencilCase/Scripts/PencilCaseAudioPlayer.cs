using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PencilCaseAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openSound;

    private void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenSound()
    {
        _audioSource.clip = _openSound;
        _audioSource.Play();
    }
}
