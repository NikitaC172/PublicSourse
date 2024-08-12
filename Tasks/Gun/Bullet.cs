using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _force;
    [SerializeField] private float _time;
    [SerializeField] private MeshRenderer _meshRenderer;

    [Inject] private Slider _slider;
    [Inject] private Transform _transform;
    [Inject] private Material _material;

    private void OnEnable()
    {
        SetPosition();
        SetMaterial();
        Shoot();
    }

    private void SetMaterial()
    {
        _meshRenderer.sharedMaterial = _material;
    }

    private void SetPosition()
    {
        _rigidbody.isKinematic = true;
        transform.position = _transform.position;
        transform.rotation = _transform.rotation;
    }

    private void Shoot()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(-Vector3.forward * (_force + _force * _slider.value));
        _ = DestroyObJect();
    }

    private async UniTask DestroyObJect()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_time));
        GameObject.Destroy(gameObject);
    }
}
