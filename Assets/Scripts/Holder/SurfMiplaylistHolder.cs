using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SurfMiplaylistHolder : ViewModel
{

    public TextMeshProUGUI playlistName;
    public Image playlistPicture;
    public TextMeshProUGUI playlistOwner;
    private string spotifyID;
    public PlaylistViewModel playlistViewModel;
    public SurfMiPlaylistViewModel miPlaylistViewModel;
    public ChangeImage change;
    public bool @public;
    public HolderManager holderManager;
    private bool enabled;
    public void SetOnSelectedPlaylist(bool _enabled) { enabled = _enabled; }
    public bool GetOnSelectedPlaylist() { return enabled; }
    public void OnEnable()
    {
        AddEventListener<OnSelectedPlaylistClick>(SelectedPlaylistEventListener);
    }
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

    public void PublicTrue()
    {
        change.True();
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
    public void SelectedPlaylistEventListener(OnSelectedPlaylistClick _enable)
    {
        SetOnSelectedPlaylist(false);
        gameObject.GetComponent<Image>().enabled = false;
    }
    public void OnClickSelected()
    {
        holderManager.playlistId = spotifyID;
        
        gameObject.GetComponent<Image>().enabled = true;
        
    }


}
