using UnityEngine.Events;

public class DieState : State
{
    public event UnityAction StartedDie;

    private void OnEnable()
    {
        StartedDie?.Invoke();
    }
}
