using UnityEngine;

namespace CraneGame
{
    public class PowerLine : MonoBehaviour
    {
        [SerializeField] private StageManager _stageManager;
        [SerializeField] private HandAController _handAController;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StructureTrigger>(out _))
            {
                Debug.LogWarning("ElectikPowerSTOP");

                if(_stageManager != null)
                {
                    _stageManager.GameFailed();
                }

                if (_handAController != null)
                {
                    _handAController.DisavbleControl();
                }

                if (_audioSource != null)
                {
                    _audioSource.PlayOneShot(_audioClip);
                }
            }
        }
    }
}
