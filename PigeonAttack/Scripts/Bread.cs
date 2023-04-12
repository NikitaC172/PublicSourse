using UnityEngine;

public class Bread : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _rootModel;
    [SerializeField] private int _reward;

    private const float TimeTurnOff = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {            
            _rootModel.SetActive(false);
            _particleSystem.Play();
            _score.SetScoreForBread(_reward);
            Invoke(nameof(TurnOff), TimeTurnOff);
        }
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
