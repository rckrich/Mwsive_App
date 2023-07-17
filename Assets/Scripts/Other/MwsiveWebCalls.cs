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
    public static long SUCCESS_RESPONSE_CODE { get { return 200; } }
    public static long AUTHORIZATION_FAILED_RESPONSE_CODE { get { return 401; } }

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

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
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
                    Debug.Log("Playlist created " + jsonResult);
                    RootMwsiveUser rootMwsiveUser = JsonConvert.DeserializeObject<RootMwsiveUser>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, rootMwsiveUser });
                    yield break;
                }
            }

            Debug.Log("Failed on crate playlist " + jsonResult);
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

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
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
                    Debug.Log("Playlist created " + jsonResult);
                    RootMwsiveLogin rootMwsiveLogin = JsonConvert.DeserializeObject<RootMwsiveLogin>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, rootMwsiveLogin });
                    yield break;
                }
            }

            Debug.Log("Failed on crate playlist " + jsonResult);
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

                if (webRequest.responseCode.Equals(AUTHORIZATION_FAILED_RESPONSE_CODE))
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
                    Debug.Log("Playlist created " + jsonResult);
                    RootMwsiveLogin rootMwsiveLogin = JsonConvert.DeserializeObject<RootMwsiveLogin>(jsonResult);
                    _callback(new object[] { webRequest.responseCode, rootMwsiveLogin });
                    yield break;
                }
            }

            Debug.Log("Failed on crate playlist " + jsonResult);
            yield break;
        }
    }

}
