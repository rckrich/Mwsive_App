using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescubrirPaginas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject Searchbar;
    public List<GameObject> scenes = new List<GameObject>();
    public List<TextMeshProUGUI> TextMesh = new List<TextMeshProUGUI>();
    public List<GameObject> Prefabs = new List<GameObject>();       
    public GameObject PrefabsPosition;
    public int numEnpantalla;
    private bool HasColorChanged= false;
    private string SearchText;
    private GameObject Instance;
    public List<GameObject> images = new List<GameObject>();
    public Sprite sprite;
    public Sprite sprite2;

    public void ChangeWindow()
    {

    }
    public void HideEscena(){
        mask[numEnpantalla].SetActive(false);
        TextMesh[numEnpantalla].color = new Color32(143,143,143,255);
        scenes[numEnpantalla].SetActive(false);
        numEnpantalla = 0;
        mask[0].SetActive(true);
        TextMesh[0].color = new Color32(255,255,255,255);
    }

    public void EleccionDeEscena(int numero)
    {
        if(numero != numEnpantalla)
        {
            numEnpantalla = numero;
            for(int i = 0; i < 7; i++)
            {
                images[i].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
                images[i].GetComponent<Image>().sprite = sprite;
                scenes[i].SetActive(false);
            }
            switch (numero)
            {
                case 0:
                    TextMesh[0].color = new Color32(255,255,255,255);
                    scenes[0].SetActive(true);
                    images[0].GetComponent<Image>().color = Color.white;
                    images[0].GetComponent<Image>().sprite = sprite2;
                    break;
                case 1:
                    TextMesh[1].color = new Color32(255,255,255,255);
                    scenes[1].SetActive(true);
                    images[1].GetComponent<Image>().color = Color.white;
                    images[1].GetComponent<Image>().sprite = sprite2;
                    break;
                case 2:
                    TextMesh[2].color = new Color32(255,255,255,255);
                    scenes[2].SetActive(true);
                    images[2].GetComponent<Image>().color = Color.white;
                    images[2].GetComponent<Image>().sprite = sprite2;
                    break;
                case 3:
                    TextMesh[3].color = new Color32(255,255,255,255);
                    scenes[3].SetActive(true);
                    images[3].GetComponent<Image>().color = Color.white;
                    images[3].GetComponent<Image>().sprite = sprite2;
                    break;
                case 4:
                    TextMesh[4].color = new Color32(255,255,255,255);
                    scenes[4].SetActive(true);
                    images[4].GetComponent<Image>().color = Color.white;
                    images[4].GetComponent<Image>().sprite = sprite2;
                    break;
                case 5:
                    TextMesh[5].color = new Color32(255,255,255,255);
                    scenes[5].SetActive(true);
                    images[5].GetComponent<Image>().color = Color.white;
                    images[5].GetComponent<Image>().sprite = sprite2;
                    break;
                case 6:
                    TextMesh[6].color = new Color32(255,255,255,255);
                    scenes[6].SetActive(true);
                    images[6].GetComponent<Image>().color = Color.white;
                    images[6].GetComponent<Image>().sprite = sprite2;
                    break;
            }
        }        
    }

    public void Search(){
        SearchText = Searchbar.GetComponent<TMP_InputField>().text;
        SpawnPrefab();
    }

    private void SpawnPrefab(){
        switch (numEnpantalla){
            case 0:
                break;
            case 1:
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Curadores_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.GetComponent<DynamicSearchPrefabsManager>().InitializeSingle(SearchText);   
                break;
            case 2:
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Songs_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.GetComponent<DynamicSearchPrefabsManager>().InitializeSingle(SearchText);   
                break;
            case 3: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Artists_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.GetComponent<DynamicSearchPrefabsManager>().InitializeSingle(SearchText);   
                break;
            case 4: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Albums_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.GetComponent<DynamicSearchPrefabsManager>().InitializeSingle(SearchText);   
                break;
            case 5: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Playlists_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.GetComponent<DynamicSearchPrefabsManager>().InitializeSingle(SearchText);   
                break;
            case 6: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Genders_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.GetComponent<DynamicSearchPrefabsManager>().InitializeSingle(SearchText);   
                break;
        }

    }

    

}
