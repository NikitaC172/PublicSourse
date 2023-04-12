using UnityEngine;

public class PencilEffects : MonoBehaviour
{
    [SerializeField] private PencilEffectsObject _pencilEffectsObjects;
    [SerializeField] private PencilObjectModel _pencilObjectModel;
    [SerializeField] private PencilObjectEdgePoint _edgePoint;
    [SerializeField] private ParticleSystem _particlePencil;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _timeLifeShootTrail = 0.5f;

    public void EnableEffects()
    {
        _pencilEffectsObjects.gameObject.SetActive(true);
    }

    public void DisableEffects(bool isNeedTrail = false)
    {
        if (isNeedTrail == false)
        {
            _pencilEffectsObjects.gameObject.SetActive(false);
        }
        else
        {
            _pencilEffectsObjects.gameObject.SetActive(true);
            _particlePencil.gameObject.SetActive(false);
        }
    }

    public void ChangeTimeLifeTrail()
    {
        _trailRenderer.time = _timeLifeShootTrail;
    }

    public void DisableObjectModel()
    {
        _pencilObjectModel.gameObject.SetActive(false);
    }

    public void SetedgePositionParent()
    {
        _particlePencil.gameObject.SetActive(false);
        _pencilEffectsObjects.transform.parent = _edgePoint.transform;
        _pencilEffectsObjects.transform.localPosition = Vector3.zero;
    }

    public void ReturnParent()
    {
        _pencilEffectsObjects.transform.parent = transform;
        _pencilEffectsObjects.transform.localPosition = Vector3.zero;
    }
}
