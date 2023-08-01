using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogInViewModel : ViewModel
{
    // Start is called before the first frame update
    [Header("Test Token")]
    [TextAreaAttribute]
    public string testRawValue = "";
    void Start()
    {
        
    }

   

    public void OnClickSpotify()
    {
        SpotifyConnectionManager.instance.StartConnection(FillTokenText);

       
    }

    public void FillTokenText(object[] _value)
    {
        testRawValue = (string)_value[0];
        NewScreenManager.instance.ChangeToMainView(ViewID.SurfViewModel, false);
        Debug.Log(NewScreenManager.instance.GetCurrentView().gameObject.name);
    }
}
