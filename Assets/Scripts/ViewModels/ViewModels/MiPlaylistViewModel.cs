using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiPlaylistViewModel : ViewModel
{
    // Start is called before the first frame update
    public GameObject playlistHolderPrefab;
    public Transform instanceParent;
    void Start()
    {
        SpotifyConnectionManager.instance.GetCurrentUserPlaylists(Callback_OnClick_GetCurrentUserPlaylists);
    }

    private void Callback_OnClick_GetCurrentUserPlaylists(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        PlaylistRoot playlistRoot = (PlaylistRoot)_value[1];

        foreach (Item item in playlistRoot.items)
        {
            MiplaylistHolder instance = GameObject.Instantiate(playlistHolderPrefab, instanceParent).GetComponent<MiplaylistHolder>();
            instance.Initialize(item.name, item.id, item.owner.display_name, item.@public);

            if (item.images != null && item.images.Count > 0)
                instance.SetImage(item.images[0].url);
            if (item.@public)
            {
                playlistHolderPrefab.GetComponentInChildren<ChangeImage>().OnClickToggle();
            }
        }
    }

    public void OnClick_BackButton()
    {
        NewScreenManager.instance.BackToPreviousView();
    }

    public void OnClick_SpawnCrearPlaylistButton()
    {
        NewScreenManager.instance.ChangeToSpawnedView("crearPlaylist");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
}
