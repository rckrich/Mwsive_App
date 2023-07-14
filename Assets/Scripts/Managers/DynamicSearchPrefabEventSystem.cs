using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicSearchPrefabEventSystem : MonoBehaviour
{
    public DescubrirPaginas Descubrir;
    public GameObject Searchbar;
    public List<GameObject> Prefabs = new List<GameObject>();       
    public GameObject PrefabsPosition;
    public GameObject MainCanvas;
    public ScrollRect ScrollBar; 
    private string SearchText;
    private GameObject Instance;
    private int numEnpantalla;
    public List<List<GameObject>> ListOfLists = new List<List<GameObject>>();
    public float MaxPrefabsinScreen = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in Prefabs)
        {
            ListOfLists.Add( new List<GameObject>());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Search(){
        numEnpantalla = Descubrir.GetCurrentEscena();
        if(ListOfLists[numEnpantalla].Count ==0){
            DynamicPrefabSpawner();
        }
        
        SearchText = Searchbar.GetComponent<TMP_InputField>().text;
        foreach (GameObject item in ListOfLists[numEnpantalla])
        { 
            item.GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(SearchText);  
        }
        
    }
    
    public void CheckForSpawn(){
        if(ListOfLists[numEnpantalla].Count != 0){
            if(ScrollBar.verticalNormalizedPosition  <= -1){
                
                DynamicPrefabSpawner();
            }
        }
        
    }
    
    public void DynamicPrefabSpawner(){
        if(MaxPrefabsinScreen ==0){
            Vector3[] v = new Vector3[4];
            MainCanvas.GetComponent<RectTransform>().GetWorldCorners(v);
            float maxY = Mathf.Max (v [0].y, v [1].y, v [2].y, v [3].y);
            SpawnPrefab();
            MaxPrefabsinScreen = Mathf.Round(maxY / ListOfLists[numEnpantalla][ListOfLists[numEnpantalla].Count -1].GetComponent<RectTransform>().sizeDelta.y);
        }
        for (int i = 0; i <= MaxPrefabsinScreen+1; i++)
        {
            SpawnPrefab();
        }

    }

    private void SpawnPrefab(){
        switch (numEnpantalla){
            case 0:
                break;
            case 1:
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Curadores_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);  
                ListOfLists[numEnpantalla].Add(Instance);
                break;
            case 2:
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Songs_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                ListOfLists[numEnpantalla].Add(Instance);   
                break;
            case 3: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Artists_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);   
                ListOfLists[numEnpantalla].Add(Instance);
                break;
            case 4: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Albums_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                ListOfLists[numEnpantalla].Add(Instance);  
                break;
            case 5: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Playlists_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                ListOfLists[numEnpantalla].Add(Instance);   
                break;
            case 6: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Genders_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                ListOfLists[numEnpantalla].Add(Instance);
                break;
        }

    }
}
