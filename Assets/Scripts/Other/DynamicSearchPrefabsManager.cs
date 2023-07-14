using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicSearchPrefabsManager : MonoBehaviour
{
    public Image _Image;

    public List<TextMeshProUGUI> TextMesh = new List<TextMeshProUGUI>();

    // Start is called before the first frame update

    public void InitializeSingle(string _text){
        
        TextMesh[0].text = _text;

    }

    public void DoubleLine(List<string> _text){
        
        TextMesh[0].text = _text[0];
        TextMesh[1].text = _text[1];

    }


}
