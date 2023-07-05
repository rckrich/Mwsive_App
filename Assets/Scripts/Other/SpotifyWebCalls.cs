using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public delegate void SpotifyWebCallback(object _value);

public static class SpotifyWebCalls
{
    public static IEnumerator CR_GetUserProfile(string _token, SpotifyWebCallback _callback)
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
                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch Profile result " + jsonResult);
                    ProfileRoot profileRoot = JsonConvert.DeserializeObject<ProfileRoot>(jsonResult);
                    _callback(profileRoot);
                    yield break;
                }
            }

            Debug.Log("Failed on fetch profile " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_FetchUserTopTracks(string _token)
    {
        string jsonResult = "";

        string url = "https://api.spotify.com/v1/me/top/tracks";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.
                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    jsonResult = webRequest.downloadHandler.text;
                    Debug.Log("Fetch User Top Songs result " + jsonResult);
                    UserTopItemsRoot userTopItemsRoot = JsonConvert.DeserializeObject<UserTopItemsRoot>(jsonResult);

                    AudioDownloadManager.instance.SetTotalTracksNumber(userTopItemsRoot.items.Count);

                    foreach (Item item in userTopItemsRoot.items)
                    {
                        if ((!item.preview_url.Equals("")) || (item.preview_url != null))
                            AudioDownloadManager.instance.AddTrackToList(item.name, item.preview_url, item.album.images[0].url, item.album.artists[0].name);
                    }

                    Debug.Log(userTopItemsRoot.ToString());
                    yield break;
                }
            }

            Debug.Log("Failed on fetch profile " + jsonResult);
            yield break;

        }
    }
}
