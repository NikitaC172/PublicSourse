using System.Collections;
using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField] private GameObject _currentPanel;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void ChangePanel(GameObject newPanel)
    {
        StartCoroutine(Change(newPanel));
    }

    private IEnumerator Change(GameObject newPanel)
    {
        float changetime = 1.0f;
        float timeCount = 0;
        float hideAlfa = 0;
        float ShowAlfa = 1;

        while (timeCount < changetime)
        {
            _canvasGroup.alpha = Mathf.Lerp(ShowAlfa, hideAlfa, timeCount / changetime);
            timeCount += Time.deltaTime;
            yield return null;
        }

        _currentPanel.SetActive(false);
        _currentPanel = newPanel;
        _currentPanel.SetActive(true);
        timeCount = 0;

        while (timeCount < changetime)
        {
            _canvasGroup.alpha = Mathf.Lerp(hideAlfa, ShowAlfa, timeCount / changetime);
            timeCount += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = ShowAlfa;
    }
}
