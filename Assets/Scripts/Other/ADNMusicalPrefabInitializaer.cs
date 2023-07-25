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
    
    public void InitializeSingleWithBackground(string _text){
        TextMesh[0].text = _text;
        int i = int.Parse(gameObject.name.Split("-")[1]);
        while (i > Colors.Count -1){
            Debug.Log(i);
            i = i-(Colors.Count -1);
            Debug.Log(i);
        }
        Color32 _color = Colors[i];
        Background.GetComponent<Image>().color =_color;
    }
    public void OnClickButton(){
        ADNDynamicScroll.instance.ShowAllInstances();
    }

    


}
