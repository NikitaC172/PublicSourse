using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BulletDI : MonoInstaller
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _transform;
    [SerializeField] private Material _material;

    public override void InstallBindings()
    {
        Container.Bind<Slider>().FromInstance(_slider).AsSingle();
        Container.Bind<Transform>().FromInstance(_transform).AsSingle();
        Container.Bind<Material>().FromInstance(_material).AsSingle();
    }
}
