using DaVanciInk.AdvancedPlayerPrefs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDefaulValue : MonoBehaviour
{
    private void Awake()
    {
        LoadDefaultValue();
    }

    public void LoadDefaultValue()
    {
        if (AdvancedPlayerPrefs.HasKey("FirstLoad") == false && AdvancedPlayerPrefs.GetInt("FirstLoad") != 1)
        {
            AdvancedPlayerPrefs.SetFloat("FirstLoad", 1);
            AdvancedPlayerPrefs.SetInt("L1-1", -1);
            AdvancedPlayerPrefs.SetInt("L1-2", -1);
            AdvancedPlayerPrefs.SetInt("L1-3", -1);
            AdvancedPlayerPrefs.SetInt("L1-4", -1);

            AdvancedPlayerPrefs.SetInt("L2-1", -1);
            AdvancedPlayerPrefs.SetInt("L2-2", -1);
            AdvancedPlayerPrefs.SetInt("L2-3", -1);
            AdvancedPlayerPrefs.SetInt("L2-4", -1);

            AdvancedPlayerPrefs.SetInt("L3-1", -1);
            AdvancedPlayerPrefs.SetInt("L3-2", -1);
            AdvancedPlayerPrefs.SetInt("L3-3", -1);
            AdvancedPlayerPrefs.SetInt("L3-4", -1);

            AdvancedPlayerPrefs.SetInt("L4-1", -1);
            AdvancedPlayerPrefs.SetInt("L4-2", -1);
            AdvancedPlayerPrefs.SetInt("L4-3", -1);
            AdvancedPlayerPrefs.SetInt("L4-4", -1);

            AdvancedPlayerPrefs.SetFloat("OffsetX", 0);
            AdvancedPlayerPrefs.SetFloat("OffsetY", 0.82f);
            AdvancedPlayerPrefs.SetFloat("OffsetZ", 0);
            AdvancedPlayerPrefs.SetFloat("ValueAssistent", 0);
            AdvancedPlayerPrefs.SetFloat("ValueMusic", 0);
            AdvancedPlayerPrefs.SetFloat("ValueSound", 0);
        }

        AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(1);
    }


}
