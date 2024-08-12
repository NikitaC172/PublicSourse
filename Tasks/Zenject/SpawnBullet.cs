using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Zenject;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] private Button _buttonShoot;
    [SerializeField] private Button _buttonReset;
    [SerializeField] private AssetReference _assetReference;

    [Inject]private DiContainer _diContainer;

    private void OnEnable()
    {
        _buttonShoot.onClick.AddListener(Shoot);
    }

    private void OnDisable()
    {
        _buttonShoot.onClick.RemoveListener(Shoot);
    }

    private void Shoot()
    {
        _ = Spawn();
    }

    public async UniTask Spawn()
    {
        GameObject gameObjectSpawn;
        AsyncOperationHandle<GameObject> gameObjects = _assetReference.LoadAssetAsync<GameObject>();
        await gameObjects.Task;
        gameObjectSpawn = gameObjects.Result;
        _diContainer.InstantiatePrefab(gameObjectSpawn);

        Addressables.ReleaseInstance(gameObjects);
    }
}
