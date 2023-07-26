using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ADNMusicalPrefabInitializaer : MonoBehaviour
{
    public Image Portada;
    public Image Background;
    public List<Color32> Colors = new List<Color32>(); 
    private bool _IsTheObjectVisible;
    

    public List<TextMeshProUGUI> TextMesh = new List<TextMeshProUGUI>();

    // Start is called before the first frame update
    
    public void InitializeSingleWithBackgroundWithImage(string _text, string Image){
        Debug.Log(gameObject.name);
        TextMesh[0].text = _text;
        int i = int.Parse(gameObject.name.Split("-")[1]);
        i--;
        while (i > Colors.Count -1){
            
            i = i-(Colors.Count -1);
            
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;

        
    
        ImageManager.instance.GetImage(Image, Portada, (RectTransform)this.transform);
    
    }
    public void InitializeSingleWithBackgroundNoImage(string _text){
        Debug.Log(gameObject.name);
        TextMesh[0].text = _text;
        int i = int.Parse(gameObject.name.Split("-")[1]);
        i--;
        while (i > Colors.Count -1){
            
            i = i-(Colors.Count -1);
            
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;
    
    }
    public void OnClickButton(){
        ADNDynamicScroll.instance.ShowAllInstances();
    }

    


}
