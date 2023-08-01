using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static log4net.Appender.RollingFileAppender;

public class SplashViewModel : ViewModel
{
    // Start is called before the first frame update
    public OAuthHandler OAuthHandler;
    public string testRawValue = "";
    public string email;
    public string profileid;
    public ProfileRoot profile;
    public string playlistid;
    public GameObject LoginViewModel;
    public string[] itemid;
    private void Awake()
    {
        
    }
    void Start()
    {
        
        if (ProgressManager.instance.progress.userDataPersistance.spotify_userTokenSetted)
        {
            
            if(ProgressManager.instance.progress.userDataPersistance.spotify_expires_at.CompareTo(DateTime.Now) > 0)
            {
                
                if (ProgressManager.instance.progress.userDataPersistance.userTokenSetted)
                {
                    if(ProgressManager.instance.progress.userDataPersistance.expires_at.CompareTo(DateTime.Now) > 0)
                    {
                        if (ProgressManager.instance.progress.userDataPersistance.current_playlist.Equals(""))
                        {
                            SpotifyConnectionManager.instance.GetCurrentUserPlaylists(Callback_GetCurrentUserPlaylists);                            
                            
                        }
                        else
                        {
                            NewScreenManager.instance.ChangeToMainView(ViewID.SurfViewModel, false);
                            Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
                        }
                    }
                }
                else
                {
                    SpotifyConnectionManager.instance.GetCurrentUserProfile(Callback_GetUserProfile);
                    
                    
                   

                }
            }
            else
            {
                SpotifyConnectionManager.instance.StartConnection(FillTokenText);
                if (ProgressManager.instance.progress.userDataPersistance.userTokenSetted)
                {
                    if (ProgressManager.instance.progress.userDataPersistance.expires_at.CompareTo(DateTime.Now) > 0)
                    {
                        if (ProgressManager.instance.progress.userDataPersistance.current_playlist == null)
                        {
                            SpotifyConnectionManager.instance.GetCurrentUserPlaylists(Callback_GetCurrentUserPlaylists);
                        }
                        else
                        {
                            NewScreenManager.instance.ChangeToMainView(ViewID.SurfViewModel, false);
                            Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
                        }
                    }
                }
                else
                {
                    SpotifyConnectionManager.instance.GetCurrentUserProfile(Callback_GetUserProfile);

                   
                    

                }
            }
        }
        else
        {
            NewScreenManager.instance.ChangeToMainView(ViewID.LogInViewModel, false);
            Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
        }
      
    }
  
    public void FillTokenText(object[] _value)
    {
        testRawValue = (string)_value[0];

    }
    private void Callback_PostCreateUser(object[] _value)
    {
        MwsiveCreatenRoot mwsiveCreatenRoot = (MwsiveCreatenRoot)_value[1];
        ProgressManager.instance.progress.userDataPersistance.access_token = mwsiveCreatenRoot.mwsive_token;
        ProgressManager.instance.progress.userDataPersistance.userTokenSetted = true;
        ProgressManager.instance.save();
        SpotifyConnectionManager.instance.CreatePlaylist(profileid, Callback_CreatePlaylist);       
    }
    private void Callback_GetUserProfile(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        ProfileRoot profileRoot = (ProfileRoot)_value[1];
        profile = profileRoot;
        profileid = profileRoot.id;
        email = profileRoot.email;
        MwsiveConnectionManager.instance.PostLogin(email, Callback_PostLogin);
    }
    private void Callback_GetCurrentUserPlaylists(object[] _value)
    {
        PlaylistRoot playlistRoot = (PlaylistRoot)_value[1];
        for(int i = 0; i <= playlistRoot.items.Count; i++)
        {
            itemid[i] = playlistRoot.items[i].id;
        }
        ProgressManager.instance.progress.userDataPersistance.current_playlist = itemid[0];
        ProgressManager.instance.save();
        NewScreenManager.instance.ChangeToMainView(ViewID.SurfViewModel, false);
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
    
    private void Callback_CreatePlaylist(object[] _value)
    {
        CreatedPlaylistRoot createdPlaylistRoot = (CreatedPlaylistRoot)_value[1];
        playlistid = createdPlaylistRoot.id;
        ProgressManager.instance.progress.userDataPersistance.current_playlist = playlistid;
        ProgressManager.instance.save();
        NewScreenManager.instance.ChangeToMainView(ViewID.SurfViewModel, false);
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
    private void Callback_PostLogin(object[] _value)
    {
        string webcode = (string)_value[0];
        if (webcode == "204")
        {
            MwsiveLoginRoot accessToken = (MwsiveLoginRoot)_value[1];
            ProgressManager.instance.progress.userDataPersistance.access_token = accessToken.mwsive_token;
            ProgressManager.instance.progress.userDataPersistance.userTokenSetted = true;
            ProgressManager.instance.save();
            if(ProgressManager.instance.progress.userDataPersistance.current_playlist.Equals(""))
            {
                SpotifyConnectionManager.instance.GetCurrentUserPlaylists(Callback_GetCurrentUserPlaylists);              
            }
            else
            {
                NewScreenManager.instance.ChangeToMainView(ViewID.SurfViewModel, false);
                Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
            }
        }
        else if (webcode == "404")
        {
            SpotifyConnectionManager.instance.GetCurrentUserPlaylists(Callback_GetCurrentUserPlaylistsNewUser);
            
            
        }
        else
        {
            //TODO Error
        }
    }
    private void Callback_GetCurrentUserPlaylistsNewUser(object[] _value)
    {
        PlaylistRoot playlistRoot = (PlaylistRoot)_value[1];
        for (int i = 0; i <= playlistRoot.items.Count; i++)
        {
            itemid[i] = playlistRoot.items[i].id;
        }

        MwsiveConnectionManager.instance.PostCreateUser(email, "null", 0, profile, itemid, Callback_PostCreateUser);
    }
}
