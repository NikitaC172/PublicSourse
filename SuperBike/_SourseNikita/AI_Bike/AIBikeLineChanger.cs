using System;
using System.Collections;
using UnityEngine;

public class AIBikeLineChanger : MonoBehaviour
{
    [SerializeField] float _timeBeforeChangeLine = 4.0f;
    [SerializeField] float _distanceCheck = 0.3f;
    private bool _isNeedChangeLine = false;
    public Action ChangedLine;

    private void DetermineRiderAhead()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _distanceCheck))
        {
            if (hit.collider.TryGetComponent<AiBikeController>(out AiBikeController aiBikeController))
            {
                if (_isNeedChangeLine == false)
                {
                    _isNeedChangeLine = true;
                    StartCoroutine(WaitBeforeChanging());
                }
            }
        }
        else
        {
            if (_isNeedChangeLine == true)
            {
                _isNeedChangeLine = false;
                StopCoroutine(WaitBeforeChanging());
            }
        }
    }

    private void FixedUpdate()
    {
        DetermineRiderAhead();
    }

    private IEnumerator WaitBeforeChanging()
    {
        var timeBeforeChange = new WaitForSecondsRealtime(_timeBeforeChangeLine);
        yield return timeBeforeChange;

        if (_isNeedChangeLine == true)
        {
            ChangedLine?.Invoke();
        }
    }
}
