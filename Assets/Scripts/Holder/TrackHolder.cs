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
    public HolderManager holderManager;
    private string trackSpotifyID;
    private string genre;
    private string artistId;
    private string previewURL;
    private string mp3URL;
    public GameObject playSimbol;

    public void Initialize(string _trackName, string _artistName, string _trackSpotifyID, string _artistId, string _uri, string _previewURL)
    {
        trackName.text = _trackName;
        artistName.text = _artistName;
        trackSpotifyID = _trackSpotifyID;
        artistId = _artistId;
        uri = _uri;      
        previewURL = _previewURL;
    }

    public void Initialize(string _trackName, string _artistName, string _pictureURL, string _artistId, string _uri, string _previewURL, string _trackSpotifyID)
    {
        trackName.text = _trackName;
        artistName.text = _artistName;
        trackSpotifyID = _trackSpotifyID;
        artistId= _artistId;
        uri= _uri;
        previewURL= _previewURL;
        ImageManager.instance.GetImage(_pictureURL, trackPicture, (RectTransform)this.transform);
    }

    public void SetImage(string _pictureURL)
    {
        ImageManager.instance.GetImage(_pictureURL, trackPicture, (RectTransform)this.transform);
    }

    public void OnClick_PlayAudioPreview()
    {
        mp3URL = previewURL;
        if (!mp3URL.Equals(""))
        {
            SpotifyPreviewAudioManager.instance.GetTrack(mp3URL);
            Playing();
        }
    }
    public void Playing()
    {
        playSimbol.SetActive(true);
    }
    
    public void OnClickSongOptions()
    {
        songOptions.trackSpotifyUris[0] = uri;
        songOptions.trackID = trackSpotifyID;
        NewScreenManager.instance.ChangeToSpawnedView("listaDeOpciones");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
}
