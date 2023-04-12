using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private Text _text;
    [SerializeField] private List<GameObject> _breads;

    public event UnityAction ShowedBread;

    private void OnEnable()
    {
        StartCoroutine(ShowScore());
    }

    private IEnumerator ShowScore()
    {
        float startScore = 0.0f;
        int currentScore = 0;
        float time = 0;
        float timeForCalculate = 2.0f;
        var waitsecondsBeforeStart = new WaitForSeconds(2.0f);
        var waitsecondsBetweenBread = new WaitForSeconds(1.0f);
        yield return waitsecondsBeforeStart;

        while (time < timeForCalculate)
        {
            time += Time.deltaTime;
            currentScore = (int)Mathf.Lerp(startScore, _score.CurrentScore, time / timeForCalculate);
            _text.text = currentScore.ToString();
            yield return null;
        }

        for (int i = 0; i < _score.CurrentBread; i++)
        {
            _breads[i].SetActive(true);
            ShowedBread?.Invoke();
            yield return waitsecondsBetweenBread;
        }
    }
}
