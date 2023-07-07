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

    private object[] pendingRequestParameters;
    private bool isPendingRequestAvailable = false;

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

    public object[] GetPendingRequestParameters()
    {
        return pendingRequestParameters;
    }

    public void DoPendingRequest()
    {
        if (!isPendingRequestAvailable) return;



    }

    #region Spotify API Call Methods

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
        isPendingRequestAvailable = false;
    }

    public void GetCurrentUserTopTracks()
    {
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserTopTracks(oAuthHandler.GetSpotifyToken().AccessToken, Callback_GetCurrentUserPlaylist));
    }

    private void Callback_GetCurrentUserTopTracks(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0]))
        {
            StartReauthentication();
            return;
        }

        Debug.Log((UserTopItemsRoot)_value[1]);
        isPendingRequestAvailable = false;
    }

    public void GetCurrentUserTopArtists()
    {
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserPlaylists(oAuthHandler.GetSpotifyToken().AccessToken, Callback_GetCurrentUserPlaylist));
    }

    private void Callback_GetCurrentUserTopArtists(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0]))
        {
            StartReauthentication();
            return;
        }

        Debug.Log((UserTopItemsRoot)_value[1]);
        isPendingRequestAvailable = false;
    }

    public void GetCurrentUserPlaylst(int _limit = 20, int _offset = 0)
    {
        pendingRequestParameters = new object[] { _limit, _offset };
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserPlaylists(oAuthHandler.GetSpotifyToken().AccessToken, Callback_GetCurrentUserPlaylist, _limit, _offset));
    }

    private void Callback_GetCurrentUserPlaylist(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0]))
        {
            StartReauthentication();
            //TODO FINISH PENDING REQUEST SYSTEM
            GetCurrentUserPlaylst((int)pendingRequestParameters[0], (int)pendingRequestParameters[1]);
            return;
        }

        Debug.Log((PlaylistRoot)_value[1]);
        isPendingRequestAvailable = false;
    }

    #endregion

    #region Private Methods

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
        StopAllCoroutines();
        isPendingRequestAvailable = true;
        ResetToken();
        StartConnection();
    }

    #endregion
}
