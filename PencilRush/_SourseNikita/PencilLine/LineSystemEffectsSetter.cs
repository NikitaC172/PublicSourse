using UnityEngine;

[RequireComponent(typeof(LineSystemPencilsCount))]
public class LineSystemEffectsSetter : MonoBehaviour
{
    [SerializeField] private LevelSystem _levelSystem;
    [SerializeField] private LineSystemPencilsCount _lineSystemPencilsCount;

    private bool _isStart = false;

    public bool IsStart => _isStart;

    private void OnValidate()
    {
        _lineSystemPencilsCount = GetComponent<LineSystemPencilsCount>();
        _levelSystem = FindObjectOfType<LevelSystem>();
    }

    private void OnEnable()
    {
        _levelSystem.Started += StartLineSystemEffects;
        _levelSystem.Finished += StopLineSystemEffects;
    }

    private void OnDisable()
    {
        _levelSystem.Started -= StartLineSystemEffects;
        _levelSystem.Finished -= StopLineSystemEffects;
    }

    private void StartLineSystemEffects()
    {
        if (_isStart == false)
        {
            int count = 1;
            int pencilInLine = 15;

            foreach (Pencil pencil in _lineSystemPencilsCount.Pencils)
            {
                if (pencil.TryGetComponent<PencilChangerSize>(out PencilChangerSize pencilChangerSize) && _isStart == false)
                {
                    pencilChangerSize.SetNewPoolObject(false);
                    pencilChangerSize.StartChanger();
                }

                if (count < pencilInLine && pencil.TryGetComponent<PencilEffects>(out PencilEffects pencilEffects))
                {
                    pencilEffects.EnableEffects();
                }

                count++;
            }

            _isStart = true;
        }
        else
        {
            StopLineSystemEffects();
        }
    }

    private void StopLineSystemEffects()
    {
        foreach (Pencil pencil in _lineSystemPencilsCount.Pencils)
        {
            if (pencil.TryGetComponent<PencilChangerSize>(out PencilChangerSize pencilChangerSize))
            {
                pencilChangerSize.StopChanger();
            }
            if (pencil.TryGetComponent<PencilEffects>(out PencilEffects pencilEffects))
            {
                bool isNeedTrail = true;
                pencilEffects.DisableEffects(isNeedTrail);
            }
            if (pencil.TryGetComponent<PencilShooter>(out PencilShooter pencilShooter))
            {
                pencilShooter.ChangeTimeForFinal();
            }
        }
    }
}
