using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    private const int DefaultCollision = 0;
    [SerializeField] private const int NoCollisionOnPlayer = 10;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ItemThrower _thrower;
    [SerializeField] private List<Collider> _colliders;
    private bool _isTaked = false;

    public bool testUnlock = false; ///

    public bool IsTaked => _isTaked;

    public void SetParent(GameObject? gameObject)
    {
        if (gameObject != null)
        {
            transform.SetParent(gameObject.transform, true);
            ChangeLayerCollison(NoCollisionOnPlayer);
            DeactivateGravity();
        }
        else
        {
            ActivateGravity();
            ChangeLayerCollison(DefaultCollision);
            transform.parent = null;
        }
    }

    public void SetGravity(bool isGravity)
    {
        _rigidbody.useGravity = isGravity;
        _rigidbody.velocity = Vector3.zero;
    }

    public void ActivateGravity()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;

        if (testUnlock)///
        {
            _thrower.enabled = false;
            _thrower.enabled = true;
        }
        else
        {
            _thrower.enabled = false;
        }///


        _isTaked = false;
    }

    public void DeactivateGravity()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        _thrower.enabled = true;
        _isTaked = true;
    }

    private void ChangeLayerCollison(int numberLayer)
    {
        gameObject.layer = numberLayer;

        if (_colliders != null)
        {
            foreach (var collider in _colliders)
            {
                collider.gameObject.layer = numberLayer;
            }
        }
    }
}
