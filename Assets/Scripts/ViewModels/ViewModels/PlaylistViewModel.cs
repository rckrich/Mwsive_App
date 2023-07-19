using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaylistViewModel : ViewModel
{
    // Start is called before the first frame update
    //public TMP_InputField playlistIDInputField;
    //public TextMeshProUGUI publicText;

    [Header("Instance Referecnes")]
    public GameObject trackHolderPrefab;
    public Transform instanceParent;
   //public int objectsToNotDestroyIndex;
    public string id;
    public TextMeshProUGUI playlistName;
    public bool @public;
    public GameObject button;
    void Start()
    {
        GetPlaylist();
    }
    public void GetPlaylist()
    {
        if (!id.Equals(""))
            SpotifyConnectionManager.instance.GetPlaylist(id, Callback_GetPLaylist);
        if (@public)
        {  
            button.GetComponent<ChangeImage>().OnClickToggle();
        }
    }
    private void InstanceTrackObjects(Tracks _tracks)
    {

        foreach (Item item in _tracks.items)
        {
            TrackHolder instance = GameObject.Instantiate(trackHolderPrefab, instanceParent).GetComponent<TrackHolder>();
            instance.Initialize(item.track.name, item.track.artists[0].name, item.track.id, item.track.artists[0].id); 
            if (item.track.album.images != null && item.track.album.images.Count > 0)
                instance.SetImage(item.track.album.images[0].url);
            //trackHolderPrefab.GetComponent<TrackViewModel>().trackID = item.track.id;
            //trackHolderPrefab.GetComponent<TrackViewModel>().GetTrack();
        }
    }
    private void Callback_GetPLaylist(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        SearchedPlaylist searchedPlaylist = (SearchedPlaylist)_value[1];
        InstanceTrackObjects(searchedPlaylist.tracks);
       
    }

    public void OnClick_BackButton()
    {
        NewScreenManager.instance.BackToPreviousView();
    }
}
