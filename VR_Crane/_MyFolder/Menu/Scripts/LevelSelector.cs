using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private string _levelName;
    [SerializeField] private LevelLoader _levelLoader;

    public void SelectLevel()
    {
        _levelLoader.StartLoadLevel(_levelName);
    }
}
