using UnityEngine;

public class RestartSceneState : State
{
    [SerializeField] private GameObject _pigeonModel;
    [SerializeField] private GameObject _particleSystem;

    private void OnEnable()
    {
        _pigeonModel.SetActive(false);
        _particleSystem.SetActive(false);
    }
}
