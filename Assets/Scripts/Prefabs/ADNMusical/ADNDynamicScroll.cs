using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADNDynamicScroll : MonoBehaviour
{
    public float MaxPrefabsInScreen = 0;
    public ScrollRect ScrollBar;
    private float ScrollbarVerticalPos =-0.001f;
    public GameObject SpawnArea, Prefabs, LastPosition, PrefabPosition, Añadir, ScrollView;
    
    private GameObject Instance;  
    public List<GameObject> Instances = new List<GameObject>(); 
    private static ADNDynamicScroll _instance;

    public string PlaceHolderText;
    public List<string> Data = new List<string>(); 

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start() {
        SpotifyConnectionManager.instance.StartConnection(FillTokenText);
        if(Data != null){
            for (int i = 0; i < Data.Count-1; i++)
            {
                DynamicPrefabSpawner(0);
                if(Data[i] != null || Data[i] != ""){
                    Instances[i].GetComponent<PF_ADNMusicalEventSystem>().SetPlaceHolder(Data[i]);
                }else{
                    Instances[i].GetComponent<PF_ADNMusicalEventSystem>().SetPlaceHolder(PlaceHolderText);
                }
                
            }
        }
    }

    public void FillTokenText(object[] _value)
    {
        Debug.Log(_value); 
    }

    public void HideAllOtherInstances(string gameObjectname){
        Debug.Log("Hide");
        foreach (GameObject item in Instances)
        {
            if(item.name != gameObjectname){
                item.SetActive(false);
            }
            
        }
        Añadir.SetActive(false);
    }

    public static ADNDynamicScroll instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ADNDynamicScroll>();
            }
            return _instance;
        }
    }


    public void ShowAllInstances(string _text){
        
        foreach (GameObject item in Instances)
        {
            
            if(item.activeSelf != true){
                Debug.Log("Show");
                item.SetActive(true);
            }else{
                item.GetComponent<PF_ADNMusicalEventSystem>().End(_text);   
                
            }
            
        }
        Añadir.SetActive(true);
    }

    public void CheckForSpawn(){
        Debug.Log(ScrollBar.verticalNormalizedPosition + " " + ScrollbarVerticalPos);
        if(Instances.Count != 0){
            if(ScrollBar.verticalNormalizedPosition  <= ScrollbarVerticalPos){
                
                DynamicPrefabSpawner(MaxPrefabsInScreen);
            }
        }
        
    }
    private void CalculateMaxPrefabToCall(){
        if(MaxPrefabsInScreen ==0){
            if(Instances.Count != 0){
                MaxPrefabsInScreen = Mathf.Round((SpawnArea.GetComponent<RectTransform>().rect.height) / Instances[Instances.Count -1].GetComponent<RectTransform>().sizeDelta.y);
            }
            

        }
    }

    public void GetData(){
        Data.Clear();
        foreach (var item in Instances )
        {
            string _data = item.GetComponent<PF_ADNMusicalEventSystem>().GetPlaceHolder();
            if(_data != PlaceHolderText){
                Data.Add(_data);
            }else{
                Data.Add(null);
            }
        }
    }


    public void DynamicPrefabSpawner(float howmanyprefabs){
        if(MaxPrefabsInScreen ==0){
            
            CalculateMaxPrefabToCall();
        }
        if(howmanyprefabs != MaxPrefabsInScreen){
            for (int i = 0; i <= howmanyprefabs; i++)
            {

                SpawnPrefab();
            }
        }else{
            for (int i = 0; i <= MaxPrefabsInScreen; i++)
            {
                
                SpawnPrefab();
            }
        }

        LastPosition.transform.SetAsLastSibling();

    }

    public void KillPrefablist(){
        foreach (GameObject Prefab in Instances)
        {
            Destroy(Prefab);
        }
        Instances.Clear();
    }

    public List<GameObject>  GetInstances(){
        return Instances;
    }

    public void SpawnPrefab(){
        
        Instance = Instantiate(Prefabs,PrefabPosition.transform.position, Quaternion.identity);
        Instance.transform.SetParent(GameObject.Find("PF_Container").transform);
        Instance.transform.localScale = new Vector3(1,1,1); 
        Instances.Add(Instance);
        Instance.name =  Prefabs.name  + "-"+ Instances.Count;
        Instance.GetComponent<PF_ADNMusicalEventSystem>().ChangeName(Instances.Count);

    }
    
}
