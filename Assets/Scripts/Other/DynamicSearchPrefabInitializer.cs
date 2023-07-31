using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicSearchPrefabInitializer : MonoBehaviour
{
    public Image Portada;
    

    public List<TextMeshProUGUI> TextMesh = new List<TextMeshProUGUI>();

    // Start is called before the first frame update

    public void InitializeSingle(string _text){
        
        TextMesh[0].text = _text;
        Portada.color = new Color32 (0,0,0,0);
        gameObject.SetActive(true);

    }
    public void InitializeSingleWithImage(string _name,  string _image){
        Debug.Log(_image);
        TextMesh[0].text = _name;
        ImageManager.instance.GetImage(_image, Portada, (RectTransform)this.transform);
        gameObject.SetActive(true);
    }

    public void DoubleLine(List<string> _text){
        
        TextMesh[0].text = _text[0];
        TextMesh[1].text = _text[1];

    }


}
