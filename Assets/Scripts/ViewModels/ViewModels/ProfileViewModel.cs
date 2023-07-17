using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileViewModel : ViewModel
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick_SpawnInsigniasButton()
    {
        NewScreenManager.instance.ChangeToSpawnedView("insignias");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }

    public void OnClick_SpawnADNButton()
    {
        NewScreenManager.instance.ChangeToSpawnedView("adn");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }

    public void OnClick_SpawnMiPlaylistButton()
    {
        NewScreenManager.instance.ChangeToSpawnedView("miPlaylist");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
    public void OnClick_SpawnPopUpButton()
    {
        NewScreenManager.instance.ChangeToSpawnedView("popUp");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
    public void OnClick_SpawnPlaylistButton()
    {
        NewScreenManager.instance.ChangeToSpawnedView("playlist");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }

    
}

