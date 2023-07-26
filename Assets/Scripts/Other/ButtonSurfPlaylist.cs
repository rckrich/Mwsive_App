using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;

public class ButtonSurfPlaylist : MonoBehaviour
{
    public HolderManager holderManager;
    public TMP_Text name;
    // Start is called before the first frame update
    void Start()
    {
        OnRecharge();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlaylistButton()
    {
        NewScreenManager.instance.ChangeToSpawnedView("surfMiPlaylist");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }

    public void OnRecharge()
    {
        holderManager.GetPlaylist();
        name.text = holderManager.playlistName;
    }
}
