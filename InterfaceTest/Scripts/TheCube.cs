using UnityEngine;

public class TheCube : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private Material _material;
    [SerializeField] private MonoBehaviour _IMovable;
    private IMovable _mover;

    private void OnValidate()
    {
        if(_IMovable is not IMovable)
        {
            Debug.LogWarning("IMovable not Set!!!");
        }
    }

    private void Awake()
    {
        _mover = (IMovable)_IMovable;
    }

    private void OnEnable()
    {
        ChangeColor(_color);
        _mover.Move();
    }

    private void OnDisable()
    {
        ChangeColor(Color.white);
    }

    private void ChangeColor(Color color)
    {
        _material.color = color;
    }
}
