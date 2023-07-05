using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotifyConnectionManager : Manager
{
    [SerializeField]
    private string oauthToken = "";

    public void StartConnection()
    {
        if (oauthToken.Equals(""))
        {

        }
    }
}
