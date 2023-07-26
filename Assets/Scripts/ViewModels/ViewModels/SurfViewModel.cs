using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfViewModel : ViewModel
{
    // Start is called before the first frame update

    public GameObject surfManager;
    public Image profilePicture;
    private string profileId;
    void Start()
    {
        surfManager.SetActive(true);
        SpotifyConnectionManager.instance.GetCurrentUserProfile(Callback_GetUserProfile);
    }
    private void OnEnable()
    {
        
    }
    private void Callback_GetUserProfile(object[] _value)
    {
        //if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        ProfileRoot profileRoot = (ProfileRoot)_value[1];
        profileId = profileRoot.id;
        ImageManager.instance.GetImage(profileRoot.images[0].url, profilePicture, (RectTransform)this.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickProfile()
    {
        NewScreenManager.instance.ChangeToSpawnedView("profile");
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
}
