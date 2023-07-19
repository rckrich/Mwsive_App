using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public delegate void MwsiveWebCallback(object[] _value);

public class MwsiveWebCalls : MonoBehaviour
{
    public static IEnumerator CR_PostCreateUser(string _email, string _token, string _genre, int _age, ProfileRoot _profile, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/signin";

        WWWForm form = new WWWForm();

        form.AddField("email", _email);
        form.AddField("genre", _genre);
        form.AddField("age", _age);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            string jsonRaw = JsonConvert.SerializeObject(_profile);

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

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
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
                    Debug.Log("Create Mwsive user " + jsonResult);
                    MwsiveUserRoot mwsiveUserRoot = JsonConvert.DeserializeObject<MwsiveUserRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, mwsiveUserRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on create mwsive user " + jsonResult);
            yield break;
        }
    }

    public static IEnumerator CR_PostLogin(string _email, string _token, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/login";

        WWWForm form = new WWWForm();

        form.AddField("email", _email);
        form.AddField("id", _token);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
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
                    Debug.Log("Mwisve login " + jsonResult);
                    MwsiveLoginRoot mwsiveLoginRoot = JsonConvert.DeserializeObject<MwsiveLoginRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, mwsiveLoginRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on mwsive log in " + jsonResult);
            yield break;
        }
    }

    public static IEnumerator CR_PostLogout(string _token, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/logout";

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    Debug.Log("Mwsive logout " + jsonResult);
                    _callback(new object[] { webRequest.responseCode, null });
                    yield break;
                }
            }

            Debug.Log("Failed on mwsive log out " + jsonResult);
            yield break;
        }
    }

    public static IEnumerator CR_GetCurrentMwsiveUser(string _token, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/me";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
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
                    Debug.Log("Fetch Mwsive User result: " + jsonResult);
                    MwsiveUserRoot mwsiveUserRoot = JsonConvert.DeserializeObject<MwsiveUserRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, mwsiveUserRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch mwsive user: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_GetMwsiveUser(string _token, string _user_id, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/get_user";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("user_id", _user_id);

        url = WebCallsUtils.AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
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
                    Debug.Log("Fetch Mwsive User result: " + jsonResult);
                    MwsiveUserRoot mwsiveUserRoot = JsonConvert.DeserializeObject<MwsiveUserRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, mwsiveUserRoot });
                    yield break;
                }
            }

            Debug.Log("Failed on fetch mwsive user: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_PostDeleteUser(string _token, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/delete_user";

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    Debug.Log("Delete Mwsive user " + jsonResult);
                    _callback(new object[] { webRequest.responseCode, null });
                    yield break;
                }
            }

            Debug.Log("Failed on delete mwsive user " + jsonResult);
            yield break;
        }
    }

    public static IEnumerator CR_PostTrackAction(string _token, int _user_id, int _track_id, string _action, float _duration, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/track_action";

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            TrackAction newAction = new TrackAction
            {
                user_id = _user_id,
                track_id = _track_id,
                action = _action,
                duration = _duration
            };

            string jsonRaw = JsonConvert.SerializeObject(newAction);

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

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
                }

                Debug.Log("Protocol Error or Connection Error on fetch profile");
                yield break;
            }
            else
            {
                while (!webRequest.isDone) { yield return null; }

                if (webRequest.isDone)
                {
                    Debug.Log("Post Track action " + jsonResult);
                    _callback(new object[] { webRequest.responseCode, null });
                    yield break;
                }
            }

            Debug.Log("Failed on post track action " + jsonResult);
            yield break;
        }
    }

    public static IEnumerator CR_GetCuratorsThatVoted(string _token, string _track_id, MwsiveWebCallback _callback, int _page = 1, int _limit = 20)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/track_curators";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("track_id", _track_id);
        parameters.Add("page", _page.ToString());
        parameters.Add("limit", _limit.ToString());

        url = WebCallsUtils.AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
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
                    Debug.Log("Fetch Mwsive Curators result: " + jsonResult);
                    MwsiveCuratorsRoot mwsiveCuratorsRoot = JsonConvert.DeserializeObject<MwsiveCuratorsRoot>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, mwsiveCuratorsRoot });
                    yield break;
                }
            }

            Debug.Log("Failed fetch Mwsive Curators result: " + jsonResult);
            yield break;

        }
    }

    public static IEnumerator CR_FollowingThatVoted(string _token, string _track_id, MwsiveWebCallback _callback)
    {
        string jsonResult = "";

        string url = "https://mwsive.com/track_following";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("track_id", _track_id);

        url = WebCallsUtils.AddParametersToURI(url + "?", parameters);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                //Catch response code for multiple requests to the server in a short timespan.

                if (webRequest.responseCode.Equals(WebCallsUtils.AUTHORIZATION_FAILED_RESPONSE_CODE))
                {
                    //TODO Response when unauthorized
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
                    Debug.Log("Fetch following that vote result: " + jsonResult);
                    int following_that_voted = JsonConvert.DeserializeObject<int>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, following_that_voted });
                    yield break;
                }
            }

            Debug.Log("Failed fetch following that vote result result: " + jsonResult);
            yield break;

        }
    }

}