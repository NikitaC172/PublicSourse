using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUnloadMarkRotate : MonoBehaviour
{
    [SerializeField] private Camera _targetVR;

    private void OnEnable()
    {
        StartCoroutine(LookAtCamera());
    }

    private void OnDisable()
    {
        StopCoroutine(LookAtCamera());
    }

    private IEnumerator LookAtCamera()
    {
        Vector3 direction;
        float timeWaitToUpdate = 1f;
        var waitSecond = new WaitForSeconds(timeWaitToUpdate);

        while (true)
        {
            direction = _targetVR.transform.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
            yield return waitSecond;
        }
    }
}
