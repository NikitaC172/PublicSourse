using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMover : MonoBehaviour
{
    [SerializeField] private Collider _colliderCapsule;
    [SerializeField] private Collider _colliderBox;
    [SerializeField] private GameObject _spineRoot;

    private void Update()
    {
        _colliderCapsule.gameObject.transform.position = _spineRoot.transform.position;
        _colliderCapsule.enabled = true;
        _colliderBox.enabled = true;
    }
}
