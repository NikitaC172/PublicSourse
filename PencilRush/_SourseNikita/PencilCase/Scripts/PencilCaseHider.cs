using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PencilCase))]
public class PencilCaseHider : MonoBehaviour
{
    [SerializeField] private PencilCase _pencilCase;
    [SerializeField] private PencilCaseObjectModel _pencilCaseObject;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private PencilCaseObjectPencils _objectPencils;
    [SerializeField] private ParticleSystem _particleSystemStars;
    [SerializeField] private Material _boxMaterial;
    [SerializeField] private Material _crossMaterial;
    [SerializeField] private Material _glassMaterial;
    [SerializeField] private Material _hideableMaterial;
    [SerializeField] private Color _glassColor;
    [SerializeField] private Color _crossColor;
    [SerializeField] private Color _newColorMaterial;
    [SerializeField] private float _timeHide = 1.5f;

    List<Material> _materials = new List<Material>();

    public Action Hided;

    private void Awake()
    {
        _hideableMaterial.color = _newColorMaterial;
        _crossMaterial.color = _crossColor;
        _glassMaterial.color = _glassColor;
    }

    private void OnEnable()
    {
        _pencilCase.OpenedCase += StartHide;
    }

    private void OnDisable()
    {
        _pencilCase.OpenedCase -= StartHide;
    }

    private void StartHide()
    {
        _materials.Add(_crossMaterial);
        _materials.Add(_hideableMaterial);
        _materials.Add(_glassMaterial);

        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        _meshRenderer.materials = _materials.ToArray();
        float time = 0;

        while (time < _timeHide)
        {            
            time += Time.deltaTime;
            _crossMaterial.color = Color.Lerp(_crossColor, new Color(_crossColor.r, _crossColor.g, _crossColor.b, 0), time / _timeHide);
            _glassMaterial.color = Color.Lerp(_glassColor, new Color(_glassColor.r, _glassColor.g, _glassColor.b, 0), time / _timeHide);
            _hideableMaterial.color = Color.Lerp(_newColorMaterial, new Color(_newColorMaterial.r, _newColorMaterial.g, _newColorMaterial.b, 0), time / _timeHide);

            yield return null;
        }

        Hided?.Invoke();
        var _particleSystemStarsMainSettings = _particleSystemStars.main;
        _particleSystemStarsMainSettings.loop = false;
        _objectPencils.transform.parent = transform;
        _pencilCaseObject.DisableObject();
        _crossMaterial.color = _crossColor;
        _glassMaterial.color = _glassColor;
        _hideableMaterial.color = _newColorMaterial;
    }
}
