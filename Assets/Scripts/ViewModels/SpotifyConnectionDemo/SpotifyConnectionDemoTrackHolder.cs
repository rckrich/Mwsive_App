using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpotifyConnectionDemoTrackHolder : MonoBehaviour
{
    public TextMeshProUGUI trackName;
    public TextMeshProUGUI artistName;
    public Image trackPicture;

    public void Initialize(string _trackName, string _artistName, string _pictureURL)
    {
        trackName.text = _trackName;
        artistName.text = _artistName;
        ImageManager.instance.GetImage(_pictureURL, trackPicture, (RectTransform)this.transform);
    }
}
