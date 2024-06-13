using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemV2 : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public Rigidbody GetRigidbody() 
    { 
        return _rigidbody; 
    }


}
