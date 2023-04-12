using UnityEngine;

public class GlassWallPartsObject : MonoBehaviour
{
    [SerializeField] private float _explosionPower;
    [SerializeField] private Rigidbody[] _rigidbodies;

    private bool _isExploded;

    private void OnEnable()
    {
        if (_isExploded)
        {
            return;
        }

        _isExploded = true;
        Vector3 origin = GetAveragePosition();

        foreach (var rigidbody in _rigidbodies)
        {
            Vector3 force = (rigidbody.transform.position - origin).normalized * _explosionPower;

            rigidbody.isKinematic = false;
            rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private Vector3 GetAveragePosition()
    {
        Vector3 position = Vector3.zero;

        foreach (var rigidbody in _rigidbodies)
        {
            position += rigidbody.transform.position;
        }

        position /= _rigidbodies.Length;
        return position;
    }
}
