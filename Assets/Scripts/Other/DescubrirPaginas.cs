using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescubrirPaginas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    
    public List<GameObject> mask = new List<GameObject>();
    public List<GameObject> scenes = new List<GameObject>();
    public List<TextMeshProUGUI> TextMesh = new List<TextMeshProUGUI>();
    public int numEnpantalla;
    private bool HasColorChanged= false;

    public void ChangeWindow()
    {

    }

    public void EleccionDeEscena(int numero)
    {
        if(numero != numEnpantalla)
        {
            numEnpantalla = numero;
            for(int i = 0; i < 7; i++)
            {
                mask[i].SetActive(false);
                TextMesh[i].color = new Color32(143,143,143,255);
                scenes[i].SetActive(false);
            }
            switch (numero)
            {
                case 0:
                    TextMesh[0].color = new Color32(255,255,255,255);
                    scenes[0].SetActive(true);
                    mask[0].SetActive(true);
                    break;
                case 1:
                    TextMesh[1].color = new Color32(255,255,255,255);
                    scenes[1].SetActive(true);
                    mask[1].SetActive(true);
                    break;
                case 2:
                    TextMesh[2].color = new Color32(255,255,255,255);
                    scenes[2].SetActive(true);
                    mask[2].SetActive(true);
                    
                    break;
                case 3:
                    TextMesh[3].color = new Color32(255,255,255,255);
                    scenes[3].SetActive(true);
                    mask[3].SetActive(true);
                    break;
                case 4:
                    TextMesh[4].color = new Color32(255,255,255,255);
                    scenes[4].SetActive(true);
                    mask[4].SetActive(true);
                    break;
                case 5:
                    TextMesh[5].color = new Color32(255,255,255,255);
                    scenes[5].SetActive(true);
                    mask[5].SetActive(true);
                    break;
                case 6:
                    TextMesh[6].color = new Color32(255,255,255,255);
                    scenes[6].SetActive(true);
                    mask[6].SetActive(true);
                    break;
            }
        }        
    }

}
