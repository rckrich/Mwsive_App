using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfViewModel : ViewModel
{
    // Start is called before the first frame update

    public GameObject surfManager;
    void Start()
    {
        surfManager.SetActive(true);
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
