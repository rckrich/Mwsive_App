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

    public void StartConnection(SpotifyWebCallback _callback = null)
    {
        if (ProgressManager.instance.progress.userDataPersistance.userTokenSetted)
        {
            string rawValue = ProgressManager.instance.progress.userDataPersistance.raw_value;
            oAuthHandler.SetSpotifyTokenRawValue(rawValue);

            if (ProgressManager.instance.progress.userDataPersistance.expires_at.CompareTo(DateTime.Now) < 0)
            {
                Debug.Log("Saved token has expired, starting refresh flow");
                oAuthHandler.SpotifyStartRefreshFlow(_callback);
            }
            else
            {
                Debug.Log("Saved token has not expired, can continue normally");
                _callback(new object[]
                {
                    oAuthHandler.GetSpotifyToken().AccessToken
                }) ;
            }
        }
        else
        {
            oAuthHandler.SpotifyStartAuthFlow(_callback);
        }
    }

    public void SaveToken(string _rawValue, long _expiresIn)
    {
        ProgressManager.instance.progress.userDataPersistance.raw_value = _rawValue;
        ProgressManager.instance.progress.userDataPersistance.expires_at = ConvertExpiresInToDateTime(_expiresIn);
        ProgressManager.instance.progress.userDataPersistance.userTokenSetted = true;
        ProgressManager.instance.save();
    }

    public void ResetToken()
    {
        ProgressManager.instance.progress.userDataPersistance.raw_value = "";
        ProgressManager.instance.progress.userDataPersistance.expires_at = new DateTime(1990, 01, 01);
        ProgressManager.instance.progress.userDataPersistance.userTokenSetted = false;
        ProgressManager.instance.save();
    }

    public bool CheckReauthenticateUser(long _responseCode)
    {
        return _responseCode.Equals(SpotifyWebCalls.AUTHORIZATION_FAILED_RESPONSE_CODE);
    }

    #region Spotify API Call Methods

    public void GetCurrentUserProfile(SpotifyWebCallback _callback = null)
    {
        _callback += Callback_GetUserProfile;
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserProfile(oAuthHandler.GetSpotifyToken().AccessToken, _callback));
    }

    private void Callback_GetUserProfile(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0])) {
            StartReauthentication();
            return;
        }

        Debug.Log(((ProfileRoot)_value[1]).display_name);
    }

    public void GetCurrentUserTopTracks(SpotifyWebCallback _callback = null)
    {
        _callback += Callback_GetCurrentUserTopTracks;
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserTopTracks(oAuthHandler.GetSpotifyToken().AccessToken, _callback));
    }

    private void Callback_GetCurrentUserTopTracks(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0]))
        {
            StartReauthentication();
            return;
        }

        Debug.Log((UserTopItemsRoot)_value[1]);
    }

    public void GetCurrentUserTopArtists(SpotifyWebCallback _callback = null)
    {
        _callback += Callback_GetCurrentUserTopArtists;
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserTopArtists(oAuthHandler.GetSpotifyToken().AccessToken, _callback));
    }

    private void Callback_GetCurrentUserTopArtists(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0]))
        {
            StartReauthentication();
            return;
        }

        Debug.Log((UserTopItemsRoot)_value[1]);
    }

    public void GetCurrentUserPlaylists(SpotifyWebCallback _callback = null, int _limit = 20, int _offset = 0)
    {
        _callback += Callback_GetCurrentUserPlaylists;
        StartCoroutine(SpotifyWebCalls.CR_GetCurrentUserPlaylists(oAuthHandler.GetSpotifyToken().AccessToken, _callback, _limit, _offset));
    }

    private void Callback_GetCurrentUserPlaylists(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0]))
        {
            StartReauthentication();
            return;
        }

        Debug.Log((PlaylistRoot)_value[1]);
    }

    public void GetUserPlaylists(string _userSpotifyID, SpotifyWebCallback _callback = null, int _limit = 20, int _offset = 0)
    {
        _callback += Callback_GetUserPlaylists;
        StartCoroutine(SpotifyWebCalls.CR_GetUserPlaylists(oAuthHandler.GetSpotifyToken().AccessToken, _callback, _userSpotifyID, _limit, _offset));
    }

    private void Callback_GetUserPlaylists(object[] _value)
    {
        if (CheckReauthenticateUser((long)_value[0]))
        {
            StartReauthentication();
            return;
        }

        Debug.Log((PlaylistRoot)_value[1]);
    }

    #endregion

    #region Private Methods

    private DateTime ConvertExpiresInToDateTime(long _secondsToAdd)
    {
        DateTime expiresAbsolute = DateTime.Now.AddSeconds(_secondsToAdd);
        return expiresAbsolute;
    }

    private void StartReauthentication()
    {
        StopAllCoroutines();
        ResetToken();
        StartConnection();
    }

    #endregion
}
