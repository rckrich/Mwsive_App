using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public delegate void SpotifyWebCallback(object[] _value);

public static class SpotifyWebCalls
{

    public static long AUTHORIZATION_FAILED_RESPONSE_CODE { get { return 401; } }

    public static IEnumerator CR_GetCurrentUserProfile(string _token, SpotifyWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/me";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch Profile result: " + jsonResult);
                    ProfileRoot profileRoot = JsonConvert.DeserializeObject<ProfileRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, profileRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch profile: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetCurrentUserTopTracks(string _token, SpotifyWebCallback _callback, string _time_range = "medium_term", int _limit = 20, int _offset = 0)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/me/top/tracks";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("time_range", _time_range);
        parameters.Add("limit", _limit.ToString());
        parameters.Add("offset", _offset.ToString());

        url = AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch user top songs result: " + jsonResult);
                    UserTopItemsRoot userTopItemsRoot = JsonConvert.DeserializeObject<UserTopItemsRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, userTopItemsRoot });

                    /*AudioDownloadManager.instance.SetTotalTracksNumber(userTopItemsRoot.items.Count);

                    foreach (Item item in userTopItemsRoot.items)
                    {
                        if ((!item.preview_url.Equals("")) || (item.preview_url != null))
                            AudioDownloadManager.instance.AddTrackToList(item.name, item.preview_url, item.album.images[0].url, item.album.artists[0].name);
                    }

                    Debug.Log(userTopItemsRoot.ToString());*/
                    yield break;
                }
            }

            Debug.Log("Failed on fetch user top songs: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetCurrentUserTopArtists(string _token, SpotifyWebCallback _callback, string _time_range = "medium_term", int _limit = 20, int _offset = 0)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/me/top/artists";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("time_range", _time_range);
        parameters.Add("limit", _limit.ToString());
        parameters.Add("offset", _offset.ToString());

        url = AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch user top artist result: " + jsonResult);
                    UserTopItemsRoot userTopItemsRoot = JsonConvert.DeserializeObject<UserTopItemsRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, userTopItemsRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch user top artist: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetCurrentUserPlaylists(string _token, SpotifyWebCallback _callback, int _limit = 20, int _offset = 0)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/me/playlists";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("limit", _limit.ToString());
        parameters.Add("offset", _offset.ToString());

        url = AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if(webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch user playlists result: " + jsonResult);
                    PlaylistRoot playlistRoot = JsonConvert.DeserializeObject<PlaylistRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, playlistRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch user playlists: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetUserPlaylists(string _token, SpotifyWebCallback _callback, string _user_id, int _limit = 20, int _offset = 0)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/users/" + _user_id + "/playlists";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("limit", _limit.ToString());
        parameters.Add("offset", _offset.ToString());

        url = AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch user " + _user_id + " playlists result: " + jsonResult);
                    PlaylistRoot playlistRoot = JsonConvert.DeserializeObject<PlaylistRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, playlistRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch user " + _user_id + "playlists: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetTrack(string _token, SpotifyWebCallback _callback, string _track_id, string _market = "ES")
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/tracks/" + _track_id;

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("market", _market);

        url = AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch track " + _track_id + " result: " + jsonResult);
                    TrackRoot trackRoot = JsonConvert.DeserializeObject<TrackRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, trackRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch track " + _track_id + ": " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetSeveralTracks(string _token, SpotifyWebCallback _callback, string[] _track_ids, string _market = "ES")
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/tracks";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("market", _market);

        url = AddParametersToURI(url + "?", parameters);

        url = AddMultipleParameterToUri(url + "&", "ids", _track_ids);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch several tracks result " + jsonResult);
                    SeveralTrackRoot severalTrackRoot = JsonConvert.DeserializeObject<SeveralTrackRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, severalTrackRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch several tracks: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_CreatePlaylist(string _token, SpotifyWebCallback _callback, string _user_id, string _playlist_name = "Mwsive Playlist", string _playlist_description = "New Mwsive playlist", bool _public = false)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/users/" + _user_id + "/playlists";

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            CreatePlaylistBodyRequestRoot bodyRequest = new CreatePlaylistBodyRequestRoot {
                name = _playlist_name,
                description = _playlist_description,
                @public = _public
            };

            string jsonRaw = JsonConvert.SerializeObject(bodyRequest);

            Debug.Log("Body request for creating a playlist is:" + jsonRaw);

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRaw);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Playlist created " + jsonResult);
                    CreatedPlaylistRoot createdPlaylistRoot = JsonConvert.DeserializeObject<CreatedPlaylistRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, createdPlaylistRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on crate playlist " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_ChangePlaylistDetails(string _token, SpotifyWebCallback _callback, string _playlist_id, string _playlist_name = "Mwsive Playlist", string _playlist_description = "New Mwsive playlist", bool _public = false)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/playlists/" + _playlist_id + "/playlists";

        CreatePlaylistBodyRequestRoot bodyRequest = new CreatePlaylistBodyRequestRoot
        {
            name = _playlist_name,
            description = _playlist_description,
            @public = _public
        };

        string jsonRaw = JsonConvert.SerializeObject(bodyRequest);

        Debug.Log("Body request for creating a playlist is:" + jsonRaw);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRaw);

        using (UnityWebRequest webRequest = UnityWebRequest.Put(url, bodyRaw))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Playlist updated");
                    yield break;
                }
            }

            Debug.Log("Faied on update playlist");
            yield break;

        }
    }

    public static IEnumerator CR_AddItemsToPlaylist(string _token, SpotifyWebCallback _callback, string _playlist_id, List<string> _uris, int _position = 0)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/playlists/" + _playlist_id + "/tracks";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("position", _position.ToString());

        url = AddParametersToURI(url + "?", parameters);

        url = AddSpotifyUrisToUri(url + "&", _uris);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            AddItemsPlaylistBodyRequestRoot bodyRequest = new AddItemsPlaylistBodyRequestRoot
            {
                position = _position,
            };

            foreach(string uri in _uris)
            {
                bodyRequest.uris.Add(uri);
            }

            string jsonRaw = JsonConvert.SerializeObject(bodyRequest);

            Debug.Log("Body request for creating a playlist is:" + jsonRaw);

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRaw);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Added tracks to playlist " + _playlist_id + " result: " + jsonResult);
                    AddItemsToPlaylistRoot addItemsToPlaylistRoot = JsonConvert.DeserializeObject<AddItemsToPlaylistRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, addItemsToPlaylistRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on add tracks to playlist " + _playlist_id + ": " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_RemoveItemsFromPlaylist(string _token, SpotifyWebCallback _callback, string _playlist_id, List<string> _uris, string _snapshot_id = "")
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/playlists/" + _playlist_id + "/tracks";

        using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
        {
            RemoveItemsPlaylistBodyRequestRoot bodyRequest = new RemoveItemsPlaylistBodyRequestRoot
            {
                snapshot_id = _snapshot_id,
            };

            foreach (string spotifyUri in _uris)
            {
                Track track = new Track { uri = spotifyUri };
                bodyRequest.tracks.Add(track);
            }

            string jsonRaw = JsonConvert.SerializeObject(bodyRequest);

            Debug.Log("Body request for creating a playlist is:" + jsonRaw);

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRaw);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Removed tracks to playlist " + _playlist_id + " result: " + jsonResult);
                    RemoveItemsToPlaylistRoot removeItemsToPlaylistRoot = JsonConvert.DeserializeObject<RemoveItemsToPlaylistRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, removeItemsToPlaylistRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on remove tracks to playlist " + _playlist_id + ": " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetRecomendations(string _token, SpotifyWebCallback _callback, string[] _seed_artists, string[] _seed_genres, string[] _seed_tracks, int _limit = 20, string _market = "ES")
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/recommendations";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("limit", _limit.ToString());
        parameters.Add("market", _market);

        url = AddParametersToURI(url + "?", parameters);

        url = AddMultipleParameterToUri(url + "&", "seed_artists", _seed_artists);
        url = AddMultipleParameterToUri(url + "&", "seed_genres", _seed_genres);
        url = AddMultipleParameterToUri(url + "&", "seed_tracks", _seed_tracks);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch recommendations result: " + jsonResult);
                    RecommendationsRoot recommendationsRoot = JsonConvert.DeserializeObject<RecommendationsRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, recommendationsRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch recommendations: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_SearchForItem(string _token, SpotifyWebCallback _callback, string _query, string[] _types, string _market = "ES", int _limit = 20, int _offset = 0, string _include_external = "audio")
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/search";

        url = url + "?" + _query;

        url.Replace(' ', '+');

        //Accepted strings are: "album", "artist", "playlist", "track", "show", "episode" and "audiobook"
        url = AddMultipleParameterToUri(url + "&", "types", _types);

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("market", _market);
        parameters.Add("limit", _limit.ToString());
        parameters.Add("offset", _offset.ToString());
        parameters.Add("include_external", _include_external);

        url = AddParametersToURI(url + "&", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    ReauthenticateUser(_callback);
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch search result: " + jsonResult);
                    SearchRoot searchRoot = JsonConvert.DeserializeObject<SearchRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, searchRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch search: " + jsonResult);
            yield break;

        }
    }

    private static string AddParametersToURI(string _uri, Dictionary<string, string> _parameters)
    {
        string url = _uri;

        foreach(KeyValuePair<string, string> kvp in _parameters)
        {
            url = url + kvp.Key + "=" + kvp.Value + "&";
        }

        url = url.TrimEnd('&');

        Debug.Log("Complete url is: " + url);

        return url;
    }

    private static string AddMultipleParameterToUri(string _uri, string _key, string[] _parameters)
    {
        string url = _uri + _key + "=";

        foreach (string track_id in _parameters)
        {
            url = url + track_id + "%2C";
        }

        url = url.Remove(url.Length - 3);

        Debug.Log("Complete url with new multiple param is: " + url);

        return url;
    }

    private static string AddSpotifyUrisToUri(string _uri, List<string> _spotifyUris)
    {
        string _modified_uris = "uris=";

        foreach (string uri in _spotifyUris)
        {
            string modifiedSpotifyUri = "";
            string[] separatedUri = uri.Split(':');

            foreach (string part in separatedUri)
            {
                modifiedSpotifyUri = modifiedSpotifyUri + part + "%3A";
            }

            modifiedSpotifyUri = modifiedSpotifyUri.Remove(modifiedSpotifyUri.Length - 3);

            _modified_uris = _modified_uris + modifiedSpotifyUri + "%2C";
        }

        _modified_uris = _modified_uris.Remove(_modified_uris.Length - 3);

        return _uri + _modified_uris;
    }

    private static void ReauthenticateUser(SpotifyWebCallback _callback)
    {
        Debug.Log("Bad or expired token. This can happen if the user revoked a token or the access token has expired. Will try yo re-authenticate the user.");
        _callback(new object[] { AUTHORIZATION_FAILED_RESPONSE_CODE });
    }
}
