using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LeaderboardAuthorizer : MonoBehaviour
{
    [SerializeField] private GameObject PanelLiderboard;
    [SerializeField] private GameObject PanelAuth;

    public void ShowPanel()
    {
        if (YandexGame.SDKEnabled == true)
        {
            if(AuthorizationCheck() == true)
            {
                PanelLiderboard.SetActive(true);
            }
            else
            {
                PanelAuth.SetActive(true);
            }
        }
    }

    private bool AuthorizationCheck()
    {
        return YandexGame.auth;
    }
}
