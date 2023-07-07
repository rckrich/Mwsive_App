using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OAuthHandler : MonoBehaviour
{
    [Header("UniWebView Reference")]
    public UniWebViewAuthenticationFlowSpotify spotifyFlow = null;

    private UniWebViewAuthenticationSpotifyToken spotifyToken = null;

    public void SpotifyStartAuthFlow()
    {
        if(spotifyFlow != null)
            spotifyFlow.StartAuthenticationFlow();
    }

    public void SpotifyStartRefreshFlow()
    {
        if (spotifyFlow != null && spotifyToken != null)
        {
            if(spotifyToken.RefreshToken != null)
            {
                spotifyFlow.StartRefreshTokenFlow(spotifyToken.RefreshToken);
                return;
            }
        }

        Debug.Log("Token is null or it has no refresh token, and thus it has started authentication flow");
        SpotifyStartAuthFlow();
    }

    public void SetSpotifyTokenRawValue(string _rawValue)
    {
        spotifyToken = UniWebViewAuthenticationTokenFactory<UniWebViewAuthenticationSpotifyToken>.Parse(_rawValue);
    }

    public UniWebViewAuthenticationSpotifyToken GetSpotifyToken()
    {
        return spotifyToken;
    }

    public void OnSpotifyTokenReceived(UniWebViewAuthenticationSpotifyToken _token)
    {
        Debug.Log("Token received: " + _token.AccessToken);

        spotifyToken = _token;

        SpotifyConnectionManager.instance.SaveToken(_token.RawValue, _token.ExpiresIn);

        SpotifyConnectionManager.instance.DoPendingRequest();
    }

    public void OnSpotifyAuthError(long errorCode, string errorMessage)
    {
        Debug.Log("Error happened: " + errorCode + " " + errorMessage);
    }

    public void OnSpotifyTokenRefreshed(UniWebViewAuthenticationSpotifyToken _token)
    {
        Debug.Log("Token received: " + _token.AccessToken);

        spotifyToken = _token;

        SpotifyConnectionManager.instance.SaveToken(_token.RawValue, _token.ExpiresIn);

        SpotifyConnectionManager.instance.DoPendingRequest();
    }

    public void OnSpotifyRefreshError(long errorCode, string errorMessage)
    {
        Debug.Log("Error happened: " + errorCode + " " + errorMessage);
    }
}