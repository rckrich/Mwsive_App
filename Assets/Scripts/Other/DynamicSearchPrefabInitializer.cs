using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicSearchPrefabInitializer : MonoBehaviour
{
    public Image Portada;
    public Image Background;
    public List<Color32> Colors = new List<Color32>(); 
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
        int i = int.Parse(gameObject.name.Split("-")[1]);
        while (i > Colors.Count -1){
            i = i-Colors.Count -1;
        }
        Background.GetComponent<Image>().color =Colors[i];
    }

    public void DoubleLine(List<string> _text){
        
        TextMesh[0].text = _text[0];
        TextMesh[1].text = _text[1];

    }


}
