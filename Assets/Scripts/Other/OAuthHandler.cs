using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OAuthHandler : MonoBehaviour
{
    [Header("UniWebView Reference")]
    public GameObject uniWebViewAuthenticationCommonFlow;

    private UniWebViewAuthenticationCommonFlow spotifyFlow = null;

    private void Start()
    {
        spotifyFlow = uniWebViewAuthenticationCommonFlow.GetComponent<UniWebViewAuthenticationCommonFlow>();
    }

    public void SpotifyStartAuthFlow()
    {
        if(spotifyFlow != null)
            spotifyFlow.StartAuthenticationFlow();
    }

    public void SpotifyStartRefreshFlow(string _refreshToken)
    {
        if (spotifyFlow != null)
            spotifyFlow.StartRefreshTokenFlow(_refreshToken);
    }

    public void SetTokenRawData(string _rawData)
    {
        var token = new UniWebViewAuthenticationGitHubToken(_rawData);
    }

    public void OnSpotifyTokenReceived(UniWebViewAuthenticationStandardToken _token)
    {
        Debug.Log("Token: " + _token);
        Debug.Log("Token received: " + _token.AccessToken);
        Debug.Log("Token raw value: " + _token.RawValue);

        if (_token.RefreshToken != null)
        {
            SpotifyStartRefreshFlow(_token.RefreshToken);
        }
        else
        {
            SpotifyConnectionManager.instance.SetOAuthToken(_token.AccessToken);
        }
    }

    public void OnSpotifyAuthError(long errorCode, string errorMessage)
    {
        Debug.Log("Error happened: " + errorCode + " " + errorMessage);
    }

    public void OnSpotifyTokenRefreshed(UniWebViewAuthenticationStandardToken _token)
    {
        Debug.Log("Token received: " + _token.AccessToken);

        SpotifyConnectionManager.instance.SetOAuthToken(_token.AccessToken);
    }

    public void OnSpotifyRefreshError(long errorCode, string errorMessage)
    {
        Debug.Log("Error happened: " + errorCode + " " + errorMessage);
    }
}
