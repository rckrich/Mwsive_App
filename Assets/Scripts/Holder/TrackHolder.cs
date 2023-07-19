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

    private string trackSpotifyID;
    private string genre;
    private string artistId;
    public void Initialize(string _trackName, string _artistName, string _trackSpotifyID, string _artistId)
    {
        trackName.text = _trackName;
        artistName.text = _artistName;
        trackSpotifyID = _trackSpotifyID;
        artistId = _artistId;
    }

    public void Initialize(string _trackName, string _artistName, string _pictureURL, string _artistId, string _trackSpotifyID)
    {
        trackName.text = _trackName;
        artistName.text = _artistName;
        trackSpotifyID = _trackSpotifyID;
        artistId= _artistId;
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
}
