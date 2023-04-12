using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizatorBoots : MonoBehaviour
{
    [SerializeField] private MeshRenderer _boot;
    [SerializeField] private MeshRenderer _bootRagdoll;
    [SerializeField] private CustomizatorSystem _customizator;

    private void OnValidate()
    {
        _boot = GetComponent<MeshRenderer>();
        _customizator = FindObjectOfType<CustomizatorSystem>();
    }

    private void OnEnable()
    {
        _customizator.BootMaterialChanged += SetBootMaterial;
    }

    private void OnDisable()
    {
        _customizator.BootMaterialChanged -= SetBootMaterial;
    }

    private void SetBootMaterial(Material bootMaterial)
    {
        _boot.material = bootMaterial;

        if (_bootRagdoll != null)
        {
            _bootRagdoll.material = bootMaterial;
        }
    }
}
