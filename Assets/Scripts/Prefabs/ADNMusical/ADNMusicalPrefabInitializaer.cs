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
    

    public TextMeshProUGUI TextMesh;

    // Start is called before the first frame update
    
    public void InitializeSingleWithBackgroundWithImage(string _text, string Image){
          
        TextMesh.text = _text;
        int i = int.Parse(gameObject.name.Split("-")[1]);
        i--;
        while (i > Colors.Count -1){
            
            i = i-(Colors.Count -1);
            
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;
        Debug.Log(Image);
        
    
        ImageManager.instance.GetImage(Image, Portada, (RectTransform)this.transform);
        gameObject.SetActive(true);
    }
    public void InitializeSingleWithBackgroundNoImage(string _text){
        
        TextMesh.text = _text;
        int i = int.Parse(gameObject.name.Split("-")[1]);
        i--;
        while (i > Colors.Count -1){
            
            i = i-(Colors.Count -1);
            
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;
        gameObject.SetActive(true);
    }


    public void OnClickButton(){
        ADNDynamicScroll.instance.ShowAllInstances(TextMesh.text);
    }

    


}
