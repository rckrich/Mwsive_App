using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpotifyConnectionDemoPlaylistsHolder : MonoBehaviour
{
    public TextMeshProUGUI playlistName;
    public Image playlistPicture;

    private string spotifyID;

    public void Initialize(string _playlistName, string _spotifyID)
    {
        playlistName.text = _playlistName;
        spotifyID = _spotifyID;
    }

    public void Initialize(string _playlistName, string _spotifyID, string _pictureURL)
    {
        playlistName.text = _playlistName;
        spotifyID = _spotifyID;
        ImageManager.instance.GetImage(_pictureURL, playlistPicture, (RectTransform)this.transform);
    }

    public void SetImage(string _pictureURL)
    {
        ImageManager.instance.GetImage(_pictureURL, playlistPicture, (RectTransform)this.transform);
    }

    public void OnClick_CopyToClipboard()
    {
        if(!spotifyID.Equals(""))
            GUIUtility.systemCopyBuffer = spotifyID;
    }
}
