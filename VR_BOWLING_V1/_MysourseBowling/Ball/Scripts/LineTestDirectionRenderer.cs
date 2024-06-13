using UnityEngine;

public class LineTestDirectionRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private ItemThrower _itemThrower;

    private void Start()
    {
        _lineRenderer.positionCount = 2;
    }

    private void FixedUpdate()
    {
        Render();
    }

    private void Render()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position + _itemThrower._TestVector.normalized);
    }
}


