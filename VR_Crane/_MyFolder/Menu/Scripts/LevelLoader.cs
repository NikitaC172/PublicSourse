using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _CanvasVRLoad;
    [SerializeField] private TMP_Text _textLoad;
    [SerializeField] private Slider _sliderLoad;
    
    public void StartLoadLevel(string LevelName)
    {
        _CanvasVRLoad.SetActive(true);
        StartCoroutine(LoadLevelAsync(LevelName));
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadLevelAsync(string LevelName)
    {
        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(LevelName);

        while(aSyncLoad.isDone == false)
        {
            _sliderLoad.value = aSyncLoad.progress;
            _textLoad.text = ((int)aSyncLoad.progress * 100).ToString() + "%";
            yield return null;
        }
    }
}
