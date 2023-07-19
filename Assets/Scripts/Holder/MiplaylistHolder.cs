using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiplaylistHolder : MonoBehaviour
{

    public TextMeshProUGUI playlistName;
    public Image playlistPicture;
    public TextMeshProUGUI playlistOwner;
    private string spotifyID;
    public PlaylistViewModel playlistViewModel;
    public bool @public;


    public void Initialize(string _playlistName, string _spotifyID, string _owner, bool _public)
    {
        playlistName.text = _playlistName;
        spotifyID = _spotifyID;
        playlistOwner.text = _owner;
        @public = _public;
    }

    public void Initialize(string _playlistName, string _spotifyID, string _owner, bool _public, string _pictureURL)
    {
        playlistName.text = _playlistName;
        spotifyID = _spotifyID;
        playlistOwner.text = _owner;
        @public = _public;
        ImageManager.instance.GetImage(_pictureURL, playlistPicture, (RectTransform)this.transform);
    }

    public void SetImage(string _pictureURL)
    {
        ImageManager.instance.GetImage(_pictureURL, playlistPicture, (RectTransform)this.transform);
    }


    public void OnClick_SpawnPlaylistButton()
    {
        playlistViewModel.playlistName.text = playlistName.text;
        playlistViewModel.id = spotifyID;
        playlistViewModel.@public = @public;
        NewScreenManager.instance.ChangeToSpawnedView("playlist");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }


}
