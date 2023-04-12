using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PencilCaseHider))]
public class PencilCaseDroper : MonoBehaviour
{
    [SerializeField] private PencilCaseHider _pencilCaseHider;
    [SerializeField] private List<Pencil> _pencils;
    [SerializeField] private LineSystemAdderPencil _lineSystemAdderPencil;
    [SerializeField] private float timeStepDrop = 0.25f;

    private void OnValidate()
    {
        _pencilCaseHider = GetComponent<PencilCaseHider>();
        _lineSystemAdderPencil = FindObjectOfType<LineSystemAdderPencil>();
    }

    private void OnEnable()
    {
        _pencilCaseHider.Hided += StartDrop;
    }

    private void OnDisable()
    {
        _pencilCaseHider.Hided -= StartDrop;
    }

    private void StartDrop()
    {
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        var waitTime = new WaitForSeconds(timeStepDrop);

        while (_pencils.Count > 0)
        {
            Pencil pencil = _pencils[Random.Range(0, _pencils.Count)];
            _pencils.Remove(pencil);
            _lineSystemAdderPencil.AddPencilFromPencilCase(pencil);
            yield return waitTime;
        }
    }
}
