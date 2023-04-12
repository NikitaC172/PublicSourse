using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LineSystemAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public AudioSource GetAudioSource()
    {
        return _audioSource;
    }
}
