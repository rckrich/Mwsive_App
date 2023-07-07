using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MwsiveControllerButtons : MonoBehaviour
{
    public SurfManager Surf;
    public void OnClickOlaButton(){
        
        GameObject Instance = Surf.GetCurrentPrefab();
        Instance.GetComponentInChildren<MwsiveButton>().OnClickOlaButton();
        Debug.Log(Instance);
    }
    public void OnClickAñadirButton(){
        GameObject Instance = Surf.GetCurrentPrefab();
        Instance.GetComponentInChildren<MwsiveButton>().OnClickAñadirButton();
    }
    public void OnClickCompartirButton(){
        GameObject Instance = Surf.GetCurrentPrefab();
        Instance.GetComponentInChildren<MwsiveButton>().OnClickCompartirButton();
    }
}
