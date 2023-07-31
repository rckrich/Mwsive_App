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
    
    List<Artists> artists = new List<Artists>();
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Subtitle;

    // Start is called before the first frame update
    
    public void InitializeSingleWithBackgroundWithImage(string _text, string Image){
        ImageManager.instance.GetImage(Image, Portada, (RectTransform)this.transform);
        Title.text = _text;
        int i = int.Parse(gameObject.name.Split("-")[1]);
        i--;
        while (i > Colors.Count -1){
            
            i = i-(Colors.Count -1);
            
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;
        Debug.Log(Image);
        
    
        
        gameObject.SetActive(true);
    }
    public void InitializeSingleWithBackgroundNoImage(string _text){
        
        Title.text = _text;
        int i = int.Parse(gameObject.name.Split("-")[1]);
        i--;
        while (i > Colors.Count -1){
            
            i = i-(Colors.Count -1);
            
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;
        gameObject.SetActive(true);
    }


     public void InitializeDoubleWithBackgroundWithImage(string _Title, List<Artists> _Artists, string Image){
        ImageManager.instance.GetImage(Image, Portada, (RectTransform)this.transform);
        Title.text = _Title;
        
        foreach (Artists item in _Artists)
        {
           Subtitle.text =item.ToString() + ", ";
        }
        
        int i = int.Parse(gameObject.name.Split("-")[1]);
        i--;
        while (i > Colors.Count -1){
            
            i = i-(Colors.Count -1);
            
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;
        Debug.Log(Image);
        
    
        
        gameObject.SetActive(true);
    }
    public void InitializeDoubleWithBackgroundNoImage(string _Title, string _Subtitle){
        
        Title.text = _Title;
        Subtitle.text = _Subtitle;
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
        ADNDynamicScroll.instance.ShowAllInstances(Title.text);
    }

    


}
