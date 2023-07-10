using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescubrirPaginas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public List<GameObject> mask = new List<GameObject>();
    public List<GameObject> scenes = new List<GameObject>();
    public int numEnpantalla;

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
                scenes[i].SetActive(false);
            }
            switch (numero)
            {
                case 0:
                    scenes[0].SetActive(true);
                    mask[0].SetActive(true);
                    break;
                case 1:
                    scenes[1].SetActive(true);
                    mask[1].SetActive(true);
                    break;
                case 2:
                    scenes[2].SetActive(true);
                    mask[2].SetActive(true);
                    break;
                case 3:
                    scenes[3].SetActive(true);
                    mask[3].SetActive(true);
                    break;
                case 4:
                    scenes[4].SetActive(true);
                    mask[4].SetActive(true);
                    break;
                case 5:
                    scenes[5].SetActive(true);
                    mask[5].SetActive(true);
                    break;
                case 6:
                    scenes[6].SetActive(true);
                    mask[6].SetActive(true);
                    break;
            }
        }
      
            
    }
}
