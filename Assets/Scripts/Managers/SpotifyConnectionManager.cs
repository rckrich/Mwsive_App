using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [Header("UniWebView OAuth Reference")]
    public OAuthHandler oAuthHandler;

    private void Start()
    {
        StartConnection();
    }

    public void StartConnection()
    {
        if (ProgressManager.instance.progress.userDataPersistance.userTokenSetted)
        {
            string rawValue = ProgressManager.instance.progress.userDataPersistance.raw_value;
            oAuthHandler.SetSpotifyTokenRawValue(rawValue);

            if (ProgressManager.instance.progress.userDataPersistance.expires_at.CompareTo(DateTime.Now) < 0)
            {
                Debug.Log("Saved token has expired, starting refresh flow");
                oAuthHandler.SpotifyStartRefreshFlow();
            }
            else
            {
                Debug.Log("Saved token has not expired, can continue normally");

                //TEST
                GetUserProfile();
            }
        }
        else
        {
            oAuthHandler.SpotifyStartAuthFlow();
        }
    }

    public void SaveToken(string _rawValue, long _expiresIn)
    {
        ProgressManager.instance.progress.userDataPersistance.raw_value = _rawValue;
        ProgressManager.instance.progress.userDataPersistance.expires_at = ConvertExpiresInToDateTime(_expiresIn);
        ProgressManager.instance.progress.userDataPersistance.userTokenSetted = true;
        ProgressManager.instance.save();

        //TEST
        GetUserProfile();
    }

    public void ResetToken()
    {
        ProgressManager.instance.progress.userDataPersistance.raw_value = "";
        ProgressManager.instance.progress.userDataPersistance.expires_at = new DateTime(1990, 01, 01);
        ProgressManager.instance.progress.userDataPersistance.userTokenSetted = false;
        ProgressManager.instance.save();
    }

    public void GetUserProfile()
    {
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserProfile(oAuthHandler.GetSpotifyToken().AccessToken, Callback_GetUserProfile));
    }

    private void Callback_GetUserProfile(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0])) {
            StartReauthentication();
            return;
        }

        Debug.Log(((ProfileRoot)_value[1]).display_name);
    }

    private DateTime ConvertExpiresInToDateTime(long _secondsToAdd)
    {
        DateTime expiresAbsolute = DateTime.Now.AddSeconds(_secondsToAdd);
        return expiresAbsolute;
    }

    private bool CheckReauthenticateUser(long _responseCode)
    {
        return _responseCode.Equals(SpotifyWebCalls.AUTHORIZATION_FAILED_RESPONSE_CODE);
    }

    private void StartReauthentication()
    {
        ResetToken();
        StartConnection();
    }
}
