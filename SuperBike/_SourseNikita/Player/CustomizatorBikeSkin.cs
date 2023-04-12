using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizatorBikeSkin : MonoBehaviour
{
    [SerializeField] private MeshRenderer _bike;
    [SerializeField] private MeshRenderer _bikeRagDoll;
    [SerializeField] private CustomizatorSystem _customizator;

    private void OnValidate()
    {
        _bike = GetComponent<MeshRenderer>();
        _customizator = FindObjectOfType<CustomizatorSystem>();
    }

    private void OnEnable()
    {
        _customizator.BikeMaterialChanged += SetBikeMaterial;
    }

    private void OnDisable()
    {
        _customizator.BikeMaterialChanged -= SetBikeMaterial;
    }

    private void SetBikeMaterial(Material bikeMaterial)
    {
        _bike.material = bikeMaterial;

        if(_bikeRagDoll != null)
        {
            _bikeRagDoll.material = bikeMaterial;
        }
    }
}
