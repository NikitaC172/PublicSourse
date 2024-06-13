using CraneGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _green;
    [SerializeField] private MeshRenderer _renderer;

    private CargoJoint _joint;

    private void SetYellowMaterial()
    {
        _renderer.material = _yellow;
    }

    private void SetGreenMaterial()
    {
        _renderer.material = _green;
    }

    private void ActivateMark()
    {
        _renderer.enabled = true;
    }

    private void DeactivateMark()
    {
        _renderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CargoJoint>(out CargoJoint cargoJoint))
        {
            SetGreenMaterial();
            _joint = cargoJoint;
            _joint.ConnectChanged += SetConnection;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CargoJoint>(out CargoJoint cargoJoint))
        {
            SetYellowMaterial();
        }
    }

    private void SetConnection(Rigidbody rigidbody)
    {
        if (rigidbody != null)
        {
            DeactivateMark();
        }
        else
        {
            ActivateMark();
            _joint.ConnectChanged -= SetConnection;
        }
    }
}
