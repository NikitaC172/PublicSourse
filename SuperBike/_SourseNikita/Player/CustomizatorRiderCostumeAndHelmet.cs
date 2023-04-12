using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizatorRiderCostumeAndHelmet : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _helmetAndCostume;
    [SerializeField] private SkinnedMeshRenderer _helmetAndCostumeRagDoll;
    [SerializeField] private CustomizatorSystem _customizator;

    private void OnValidate()
    {
        _helmetAndCostume = GetComponent<SkinnedMeshRenderer>();
        _customizator = FindObjectOfType<CustomizatorSystem>();
    }

    private void OnEnable()
    {
        _customizator.HelmetMaterialChanged += SetHelmet;
        _customizator.CostumeMaterialChanged += SetCostume;
    }

    private void OnDisable()
    {
        _customizator.HelmetMaterialChanged -= SetHelmet;
        _customizator.CostumeMaterialChanged -= SetCostume;
    }

    private void SetHelmet(Material helmetMaterial)
    {
        Material[] materials = new Material[2];
        materials[0] = _helmetAndCostume.material;
        materials[1] = helmetMaterial;
        _helmetAndCostume.materials = materials;

        if (_helmetAndCostumeRagDoll != null)
        {
            _helmetAndCostumeRagDoll.materials = materials;
        }
    }

    private void SetCostume(Material costumeMaterial)
    {
        _helmetAndCostume.material = costumeMaterial;

        if (_helmetAndCostumeRagDoll != null)
        {
            _helmetAndCostumeRagDoll.material = costumeMaterial;
        }
    }
}
