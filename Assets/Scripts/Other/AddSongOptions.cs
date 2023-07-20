using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddSongOptions : ViewModel
{
    public string playlistID;
    public List<string> trackSpotifyUris = new List<string>();

    [Header("Instance References")]
    public GameObject trackHolderPrefab;
    public Transform instanceParent;
    public int objectsToNotDestroyIndex;
    public HolderManager holderManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnClick_AddItemsToPlaylist()
    {
        
        playlistID = holderManager.playlistId;
        if (!playlistID.Equals(""))
            SpotifyConnectionManager.instance.AddItemsToPlaylist(playlistID, trackSpotifyUris, Callback_OnCLick_AddItemsToPlaylist);
    }

    private void Callback_OnCLick_AddItemsToPlaylist(object[] _value)
    {
        //if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        //SpotifyConnectionManager.instance.GetPlaylist(playlistID, Callback_OnCLick_GetPlaylist);
    }
    public void OnClick_BackButton()
    {
        NewScreenManager.instance.BackToPreviousView();
    }

}
