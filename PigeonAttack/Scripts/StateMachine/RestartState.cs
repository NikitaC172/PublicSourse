using UnityEngine;

public class RestartState : State
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private ColliderHandler _colliderHandler;
    [SerializeField] private float _delayBeforeRestart = 2.0f;

    private Transform _pointForSpawn;

    private void OnEnable()    
    {
        Invoke(nameof(ActivateEventRestart), _delayBeforeRestart);
    }

    private void ActivateEventRestart()
    {
        _pointForSpawn = _spawner.GetPoint();
        _colliderHandler.gameObject.SetActive(false);
        transform.position = _pointForSpawn.position;
        transform.LookAt(_pointForSpawn);
        _colliderHandler.gameObject.SetActive(true);
        _colliderHandler.enabled = true;
    }
}
