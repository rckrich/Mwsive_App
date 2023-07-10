using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpotifyConnectionDemoPlaylistsHolder : MonoBehaviour
{
    public TextMeshProUGUI playlistName;
    public Image playlistPicture;

    public void Initialize(string _playlistName, string _pictureURL)
    {
        playlistName.text = _playlistName;
        ImageManager.instance.GetImage(_pictureURL, playlistPicture, (RectTransform)this.transform);
    }
}
