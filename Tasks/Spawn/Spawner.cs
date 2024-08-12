using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Spawner : MonoBehaviour
{
    [SerializeField] private AssetReference _prefab;
    [SerializeField] private int _maxSpawnObject = 10;
    [SerializeField] private Vector2 _randomRangeSeconds;

    private int _spawnCount = 0;
    private Material _material;
    private List<AsyncOperationHandle> _gameObjects;
    private bool _isActive = false;
    public Action Complited;

    private void OnDisable()
    {
        if (_material != null)
        {
            _material.color = Color.white;
        }
    }

    public void StartSpawn()
    {
        _spawnCount = 0;
        _ = Spawn();
    }

    public void RemoveObjects()
    {
        foreach (var obj in _gameObjects)
        {
            Addressables.ReleaseInstance(obj);
        }

        _prefab.ReleaseAsset();
        _gameObjects.Clear();
        _material.color = Color.white;
    }

    public void CancelTask()
    {
        _isActive = false;
    }

    public async UniTask Spawn()
    {
        _isActive = true;
        _gameObjects = new List<AsyncOperationHandle>();
        _spawnCount = 0;
        GameObject gameObjectSpawn;

        AsyncOperationHandle<GameObject> objectSpawn = _prefab.LoadAssetAsync<GameObject>();
        await objectSpawn.Task;
        gameObjectSpawn = objectSpawn.Result;
        _material = gameObjectSpawn.GetComponent<MeshRenderer>().sharedMaterial;

        float startPosSpawn = 0.6f;
        float deltaPosSpawn = 1.0f;

        while (_spawnCount < _maxSpawnObject && _isActive == true)
        {
            float delaySeconds = UnityEngine.Random.Range(_randomRangeSeconds.x, _randomRangeSeconds.y);
            await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds));

            if (_isActive == true)
            {
                Vector3 position = new Vector3(transform.position.x, startPosSpawn + deltaPosSpawn * _spawnCount, transform.position.z);
                AsyncOperationHandle spawn = _prefab.InstantiateAsync(position, Quaternion.identity);
                _gameObjects.Add(spawn);
                _spawnCount++;
            }
        }

        if (_isActive == true)
        {
            _material.color = Color.green;
            _isActive = false;
        }

        Complited?.Invoke();
    }
}
