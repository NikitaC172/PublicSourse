using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCullingDistances : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float[] _cullingDistances = new float[32];

    private void OnValidate()
    {
        _camera = GetComponent<Camera>();
        SetCullingDistances();
    }

    private void Awake()
    {
        SetCullingDistances();
    }

    private void SetCullingDistances()
    {
        _camera.layerCullDistances = _cullingDistances;
    }
}
