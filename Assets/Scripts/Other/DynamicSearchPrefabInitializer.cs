using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicSearchPrefabInitializer : MonoBehaviour
{
    public Image Portada;
    private bool _IsTheObjectVisible;
    

    public List<TextMeshProUGUI> TextMesh = new List<TextMeshProUGUI>();

    // Start is called before the first frame update

    public bool IsTheObjectVisible(){
        return _IsTheObjectVisible;
    }

    private void OnBecameVisible() {
        Debug.Log("Visible");
        _IsTheObjectVisible = true;
    }
    
    private void OnBecameInvisible() {
        Debug.Log("Invisible");
        _IsTheObjectVisible = false;
    }
    public void InitializeSingle(string _text){
        
        TextMesh[0].text = _text;

    }
    public void InitializeSingleWithBackground(string _text){
        TextMesh[0].text = _text;
        
        
    }

    public void DoubleLine(List<string> _text){
        
        TextMesh[0].text = _text[0];
        TextMesh[1].text = _text[1];

    }


}
