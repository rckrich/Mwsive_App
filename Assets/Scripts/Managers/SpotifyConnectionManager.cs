using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotifyConnectionManager : Manager
{
    private static SpotifyConnectionManager _instance;

    public static SpotifyConnectionManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SpotifyConnectionManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private string oauthToken = "";

    [Header("UniWebView OAuth Reference")]
    public OAuthHandler oAuthHandler;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        StartConnection();
    }

    public void StartConnection()
    {
        if (!ProgressManager.instance.progress.userDataPersistance.userTokenSetted)
        {
            if (oauthToken.Equals(""))
            {
                oAuthHandler.SpotifyStartAuthFlow();
            }
        }
        else
        {
            oauthToken = ProgressManager.instance.progress.userDataPersistance.access_token;
            GetUserProfile();
        }
    }

    public void SetOAuthToken(string _token)
    {
        oauthToken = _token;

        /*ProgressManager.instance.progress.userDataPersistance.access_token = oauthToken;
        ProgressManager.instance.progress.userDataPersistance.userTokenSetted = true;
        ProgressManager.instance.save();*/

        GetUserProfile();
    }

    public void GetUserProfile()
    {
        StartCoroutine(SpotifyWebCalls.CR_GetUserProfile(oauthToken, Callback_GetUserProfile));
    }

    private void Callback_GetUserProfile(object _value)
    {
        Debug.Log(((ProfileRoot)_value).display_name);
    }
}
