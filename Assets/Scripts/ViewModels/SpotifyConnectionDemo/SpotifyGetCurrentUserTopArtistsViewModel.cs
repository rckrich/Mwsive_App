using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotifyGetCurrentUserTopArtistsViewModel : MonoBehaviour
{
    public GameObject artistHolderPrefab;
    public Transform instanceParent;

    public void OnClick_GetCurrentUserTopArtists()
    {
        SpotifyConnectionManager.instance.GetCurrentUserTopArtists(Callback_OnCLick_GetUserTopArtists);
    }

    private void Callback_OnCLick_GetUserTopArtists(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        ResetScrollView();

        UserTopItemsRoot userTopItemsRoot = (UserTopItemsRoot)_value[1];

        foreach (Item item in userTopItemsRoot.items)
        {
            SpotifyConnectionDemoArtistHolder instance = GameObject.Instantiate(artistHolderPrefab, instanceParent).GetComponent<SpotifyConnectionDemoArtistHolder>();
            instance.Initialize(item.name, item.images[0].url);
        }
    }

    private void ResetScrollView()
    {
        for (int i = 0; i < instanceParent.childCount; i++)
        {
            Destroy(instanceParent.GetChild(i).gameObject);
        }
    }
}
