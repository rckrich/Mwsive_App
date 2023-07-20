using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackHolder : ViewModel
{
    public TextMeshProUGUI trackName;
    public TextMeshProUGUI artistName;
    public Image trackPicture;
    public TrackViewModel trackViewModel;
    public string uri;
    public AddSongOptions songOptions;

    private string trackSpotifyID;
    private string genre;
    private string artistId;
    public void Initialize(string _trackName, string _artistName, string _trackSpotifyID, string _artistId, string _uri)
    {
        trackName.text = _trackName;
        artistName.text = _artistName;
        trackSpotifyID = _trackSpotifyID;
        artistId = _artistId;
        uri = _uri;
    }

    public void Initialize(string _trackName, string _artistName, string _pictureURL, string _artistId, string _uri, string _trackSpotifyID)
    {
        trackName.text = _trackName;
        artistName.text = _artistName;
        trackSpotifyID = _trackSpotifyID;
        artistId= _artistId;
        uri= _uri;
        ImageManager.instance.GetImage(_pictureURL, trackPicture, (RectTransform)this.transform);
    }

    public void SetImage(string _pictureURL)
    {
        ImageManager.instance.GetImage(_pictureURL, trackPicture, (RectTransform)this.transform);
    }

    public void OnClickSong()
    {
        trackViewModel.trackID = trackSpotifyID;
        //trackViewModel.genre = genre;
        trackViewModel.artistId = artistId;
        NewScreenManager.instance.ChangeToSpawnedView("cancion");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }

    public void OnClickSongOptions()
    {
        songOptions.trackSpotifyUris[0] = uri;
        NewScreenManager.instance.ChangeToSpawnedView("listaDeOpciones");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
}
