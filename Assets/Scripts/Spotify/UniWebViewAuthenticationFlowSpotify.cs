using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class UniWebViewAuthenticationSpotify : UniWebViewAuthenticationCommonFlow, IUniWebViewAuthenticationFlow<UniWebViewAuthenticationSpotifyToken>
{
    public UnityEvent<UniWebViewAuthenticationSpotifyToken> OnAuthenticationFinished => throw new NotImplementedException();

    public UnityEvent<long, string> OnAuthenticationErrored => throw new NotImplementedException();

    public UnityEvent<UniWebViewAuthenticationSpotifyToken> OnRefreshTokenFinished => throw new NotImplementedException();

    public UnityEvent<long, string> OnRefreshTokenErrored => throw new NotImplementedException();

    /// <summary>
    /// Starts the authentication flow with the standard OAuth 2.0.
    /// This implements the abstract method in `UniWebViewAuthenticationCommonFlow`.
    /// </summary>
    public override void StartAuthenticationFlow()
    {
        var flow = new UniWebViewAuthenticationFlow<UniWebViewAuthenticationSpotifyToken>(this);
        flow.StartAuth();
    }

    /// <summary>
    /// Starts the refresh flow with the standard OAuth 2.0.
    /// This implements the abstract method in `UniWebViewAuthenticationCommonFlow`.
    /// </summary>
    /// <param name="refreshToken">The refresh token received with a previous access token response.</param>
    public override void StartRefreshTokenFlow(string refreshToken)
    {
        var flow = new UniWebViewAuthenticationFlow<UniWebViewAuthenticationSpotifyToken>(this);
        flow.RefreshToken(refreshToken);
    }

    public UniWebViewAuthenticationSpotifyToken GenerateTokenFromExchangeResponse(string exchangeResponse)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> GetAccessTokenRequestParameters(string authResponse)
    {
        throw new NotImplementedException();
    }

    public UniWebViewAuthenticationConfiguration GetAuthenticationConfiguration()
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> GetAuthenticationUriArguments()
    {
        throw new NotImplementedException();
    }

    public string GetCallbackUrl()
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> GetRefreshTokenRequestParameters(string refreshToken)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// The authentication flow's optional settings for GitHub.
/// </summary>
[Serializable]
public class UniWebViewAuthenticationFlowSpotifyOptional
{
    /// <summary>
    /// The redirect URI should be used in exchange token request.
    /// </summary>
    public string redirectUri = "";
    /// <summary>
    /// Suggests a specific account to use for signing in and authorizing the app.
    /// </summary>
    public string login = "";
    /// <summary>
    /// The scope string of all your required scopes.
    /// </summary>
    public string scope = "";
    /// <summary>
    /// Whether to enable the state verification. If enabled, the state will be generated and verified in the
    /// authentication callback.
    /// </summary>
    public bool enableState = false;
    /// <summary>
    /// Whether or not unauthenticated users will be offered an option to sign up for GitHub during the OAuth flow.
    /// </summary>
    public bool allowSignup = true;
}

/// <summary>
/// The token object from GitHub.
/// </summary>
public class UniWebViewAuthenticationSpotifyToken
{
    /// <summary>The access token retrieved from the service provider.</summary>
    public string AccessToken { get; }

    /// <summary>The granted scopes of the token.</summary>
    public string Scope { get; }

    /// <summary>The token type. Usually `bearer`.</summary>
    public string TokenType { get; }

    /// <summary>The refresh token retrieved from the service provider.</summary>
    public string RefreshToken { get; }

    /// <summary>Expiration duration for the refresh token.</summary>
    public long RefreshTokenExpiresIn { get; }

    /// <summary>
    /// The raw value of the response of the exchange token request.
    /// If the predefined fields are not enough, you can parse the raw value to get the extra information.
    /// </summary>
    public string RawValue { get; }

    public UniWebViewAuthenticationSpotifyToken(string result)
    {
        /*RawValue = result;
        var values = UniWebViewAuthenticationUtils.ParseFormUrlEncodedString(result);
        AccessToken = values.ContainsKey("access_token") ? values["access_token"] : null;
        if (AccessToken == null)
        {
            throw AuthenticationResponseException.InvalidResponse(result);
        }
        Scope = values.ContainsKey("scope") ? values["scope"] : null;
        TokenType = values.ContainsKey("token_type") ? values["token_type"] : null;
        RefreshToken = values.ContainsKey("refresh_token") ? values["refresh_token"] : null;
        RefreshTokenExpiresIn = values.ContainsKey("refresh_token_expires_in") ? long.Parse(values["refresh_token_expires_in"]) : 0;*/
    }
}