using UnityEngine;
using UnityEngine.Events;

public class CompleteState : State
{
    public event UnityAction Complited;

    private void OnEnable()
    {
        Complited?.Invoke();
        transform.Translate(Vector3.zero);
    }
}
