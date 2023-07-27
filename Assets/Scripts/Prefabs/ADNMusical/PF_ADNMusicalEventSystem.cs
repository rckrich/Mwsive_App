using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PF_ADNMusicalEventSystem : MonoBehaviour
{

    public string[] types = new string[] {"artist"};

    public GameObject PlaceHolder, DynamicScroll;
    private string SearchText;
    public  TMP_InputField searchbar;
    public TextMeshProUGUI Number;
    public int MaxNumerofPrefabsInstanciate;
    private bool EnableSerach = false;
    private int PositionInSearch = 0;

    [Header("Dynamic Scroll")]
    public ScrollRect ScrollBar;
    private float ScrollbarVerticalPos =-0.001f;
    public GameObject Prefabs, LastPosition, Container;
    private GameObject Instance;  
    public List<GameObject> Instances = new List<GameObject>();
    private bool CheckForSpawnHasEnded = true;


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)){
            EnableSerach = true;
            Search();
        }else{
            EnableSerach = false;
        }
    }

    public void ChangeName(int num ){
        Number.text = "#" +num;
        
    }
    

    public void Search(){
        SearchText = searchbar.text;
        if(SearchText.Length >= 1){

            PlaceHolder.SetActive(false);
            
            
            ADNDynamicScroll.instance.HideAllOtherInstances(gameObject.name);
            if(SearchText.Length >= 3 || EnableSerach){
                
                DynamicScroll.transform.DOScaleY(1, 0.5F).OnComplete(() => {//StartCoroutine(UpdateLayoutGroup());    
                });
                
                if (Instances.Count !=0){
                    KillPrefablist();       
                }
                SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_SearchForItem, "ES", MaxNumerofPrefabsInstanciate);
                DynamicPrefabSpawner();
                PositionInSearch = MaxNumerofPrefabsInstanciate;
                ScrollBar.verticalNormalizedPosition = 1;
                
            }
        }
    }

    public string GetPlaceHolder(){
        return PlaceHolder.GetComponent<TextMeshProUGUI>().text;
    }
    public void SetPlaceHolder(string _text){
        PlaceHolder.GetComponent<TextMeshProUGUI>().text = _text;
    }

    IEnumerator UpdateLayoutGroup()
    {
        gameObject.GetComponent<VerticalLayoutGroup>().enabled= false;
    
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<VerticalLayoutGroup>().enabled= true;
    }

    public void End(string _text){
        Debug.Log("eND");
        KillPrefablist();
        PlaceHolder.GetComponent<TextMeshProUGUI>().text = _text;
        
        searchbar.text = "";
        DynamicScroll.transform.DOScaleY(0, 0.5F);
        
        PlaceHolder.SetActive(true);
        
    }

    

    private void Callback_OnCLick_SearchForItem(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        SearchRoot searchRoot = (SearchRoot)_value[1];
        Debug.Log(searchRoot.artists.items.Count-1);
        if (searchRoot.artists != null){
            for (int i = 0; i < searchRoot.artists.items.Count; i++)
            {
                if (searchRoot.artists.items[i].images != null && searchRoot.artists.items[i].images.Count > 0){
                    Instances[i].GetComponent<ADNMusicalPrefabInitializaer>().InitializeSingleWithBackgroundWithImage(searchRoot.artists.items[i].name, searchRoot.artists.items[i].images[0].url);
                
                }else{
                    Instances[i].GetComponent<ADNMusicalPrefabInitializaer>().InitializeSingleWithBackgroundNoImage(searchRoot.artists.items[i].name);
                }
                
            }
        }
    }

    private void Callback_OnCLick_CheckForSpawn(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        SearchRoot searchRoot = (SearchRoot)_value[1];
        Debug.Log(PositionInSearch + " " + (Instances.Count));
        if (searchRoot.artists != null){
            for (int i = PositionInSearch; i < Instances.Count; i++)
            {
                if (searchRoot.artists.items[i-PositionInSearch].images != null && searchRoot.artists.items[i-PositionInSearch].images.Count > 0){
                    Instances[i].GetComponent<ADNMusicalPrefabInitializaer>().InitializeSingleWithBackgroundWithImage(searchRoot.artists.items[i-PositionInSearch].name, searchRoot.artists.items[i-PositionInSearch].images[0].url);
                
                }else{
                    Instances[i].GetComponent<ADNMusicalPrefabInitializaer>().InitializeSingleWithBackgroundNoImage(searchRoot.artists.items[i-PositionInSearch].name);
                }
                
            }
        }
        CheckForSpawnHasEnded = true;
        PositionInSearch += MaxNumerofPrefabsInstanciate;
    }


    public void CheckForSpawn(){
        Debug.Log("PosinSearch" + PositionInSearch);
        //Debug.Log(ScrollBar.verticalNormalizedPosition + " " + ScrollbarVerticalPos);
        if(Instances.Count != 0 && CheckForSpawnHasEnded){
            if(ScrollBar.verticalNormalizedPosition  <= ScrollbarVerticalPos){
                CheckForSpawnHasEnded = false;
                DynamicPrefabSpawner();
                SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_CheckForSpawn, "ES", MaxNumerofPrefabsInstanciate, PositionInSearch);
                
                

            }
        }
        
    }



    public void DynamicPrefabSpawner(){
        for (int i = 0; i <= MaxNumerofPrefabsInstanciate-1; i++)
        {
            SpawnPrefab();
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

    public List<GameObject> GetInstances(){
        return Instances;
    }

    public void SpawnPrefab(){
        
        Instance = Instantiate(Prefabs,LastPosition.transform.position, Quaternion.identity);
        Instance.SetActive(false);
        Instance.transform.SetParent(GameObject.Find(Container.name).transform);
        Instance.transform.localScale = new Vector3(1,1,1); 
        Instances.Add(Instance);
        Instance.name =  Prefabs.name  + "-"+ Instances.Count;

    }









}
