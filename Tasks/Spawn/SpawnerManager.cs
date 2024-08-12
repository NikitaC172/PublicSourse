using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] Button _buttonAllTask;
    [SerializeField] Button _buttonOneTask;
    [SerializeField] Button _buttonTwoTask;
    [SerializeField] Button _buttonReset;

    private List<UniTask> _tasks;
    private int _count = 0;

    public Action Complited;

    private void OnEnable()
    {
        _buttonAllTask.onClick.AddListener(StartAllTaskComlite);
        _buttonOneTask.onClick.AddListener(StartOneTaskComlite);
        _buttonTwoTask.onClick.AddListener(StartTwoTaskComlite);
        _buttonReset.onClick.AddListener(ResetState);
    }

    private void OnDisable()
    {
        _buttonAllTask.onClick.RemoveListener(StartAllTaskComlite);
        _buttonOneTask.onClick.RemoveListener(StartOneTaskComlite);
        _buttonTwoTask.onClick.RemoveListener(StartTwoTaskComlite);
        _buttonReset.onClick.RemoveListener(ResetState);
    }

    private void PrepareTasks()
    {
        _tasks = new List<UniTask>();

        for (int i = 0; i < _spawners.Count; i++)
        {
            _tasks.Add(_spawners[i].Spawn());
        }
    }

    public void ResetState()
    {
        foreach (Spawner spawn in _spawners)
        {
            spawn.RemoveObjects();
        }
    }

    public void StartAllTaskComlite()
    {
        _ = StartSpawnersAllComplite();
    }

    public void StartOneTaskComlite()
    {
        _ = StartSpawnersOneComplite();
    }

    public void StartTwoTaskComlite()
    {
        _ = StartSpawnersTwoComplite();
    }

    private async UniTask StartSpawnersAllComplite()
    {
        PrepareTasks();
        await UniTask.WhenAll(_tasks);
        Complited?.Invoke();
    }

    private async UniTask StartSpawnersTwoComplite()
    {
        PrepareTasks();
        Subscription();
        _count = 0;
        await UniTask.WhenAny(_tasks);
        await UniTask.WaitWhile(() => _count < 2);
        StopTasks();
        UnSubscription();
        Complited?.Invoke();
    }

    private void Subscription()
    {
        foreach (var spawn in _spawners)
        {
            spawn.Complited += IncreaseCount;
        }
    }

    private void UnSubscription()
    {
        foreach (var spawn in _spawners)
        {
            spawn.Complited -= IncreaseCount;
        }
    }

    private void IncreaseCount()
    {
        _count++;
    }

    private async UniTask StartSpawnersOneComplite()
    {
        PrepareTasks();
        await UniTask.WhenAny(_tasks);
        StopTasks();
        Complited?.Invoke();
    }

    private void StopTasks()
    {
        foreach (Spawner _spawner in _spawners)
        {
            _spawner.CancelTask();
        }
    }
}
