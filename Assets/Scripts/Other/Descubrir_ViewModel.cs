using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Descubrir_ViewModel : MonoBehaviour
{
    public string[] types = new string[] {"album", "artist", "playlist", "track", "show", "episode", "audiobook"};
    public DescubrirPaginas Descubrir;
    public List<GameObject> Prefabs = new List<GameObject>();    
    public List<GameObject> LastPosition = new List<GameObject>();   
    public GameObject PrefabsPosition,SpawnArea;
    public TMP_InputField Searchbar;
    public List<ScrollRect> Scrollbar = new List<ScrollRect>(); 
    public List<List<GameObject>> ListOfLists = new List<List<GameObject>>();
    private string SearchText;
    private GameObject Instance;
    public int numEnpantalla, PositionInSearch;
    private bool EnableSerach;
    public int MaxPrefabsinScreen = 0;
    private float ScrollbarVerticalPos =-0.001f;
    private bool CheckForSpawnHasEnded = true;
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
        if (Input.GetKeyUp(KeyCode.Return)){
            EnableSerach = true;
            Search();
        }else{
            EnableSerach = false;
        }
    }

    

    public void Search(){
        int lastnum= numEnpantalla;
        numEnpantalla = Descubrir.GetCurrentEscena();
        SearchText = Searchbar.text;
        Debug.Log(SearchText.Length);
        if(SearchText.Length >= 3 || EnableSerach){
            if(numEnpantalla != lastnum){
                KillPrefablist(lastnum);
            }
            if(ListOfLists[numEnpantalla].Count ==0){
                CalculateMaxPrefabToCall();
            }
            if(ListOfLists[numEnpantalla].Count !=0){
                KillPrefablist(numEnpantalla);
            }
            SpotifySearch();
        } 
    }


    private void SpotifySearch(){

        switch (numEnpantalla)
        {
            case 0:
                types = new string[] { "album", "artist", "playlist", "track", "show", "episode", "audiobook" };
                SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_SearchForItem, "ES", MaxPrefabsinScreen);
                break;
            case 1:
                
                //MWsive DataBase
                break;
            case 2:
                types = new string[] { "track" };
               SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_SearchForItem, "ES", MaxPrefabsinScreen);
               DynamicPrefabSpawner(MaxPrefabsinScreen);
                break;
            case 3:
                types = new string[] { "artist" };
               SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_SearchForItem, "ES", MaxPrefabsinScreen);
               DynamicPrefabSpawner(MaxPrefabsinScreen);
                break;
            case 4:
                types = new string[] { "album" };
               SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_SearchForItem, "ES", MaxPrefabsinScreen);
               DynamicPrefabSpawner(MaxPrefabsinScreen);
                break;
            case 5:
                types = new string[] { "playlist" };
                SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_SearchForItem, "ES", MaxPrefabsinScreen);
                DynamicPrefabSpawner(MaxPrefabsinScreen);
                break;
        }

        
        PositionInSearch = MaxPrefabsinScreen;
        //ScrollBar.verticalNormalizedPosition = 1;
    }

    private void Callback_OnCLick_SearchForItem(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        SearchRoot searchRoot = (SearchRoot)_value[1];
        
        switch (numEnpantalla)
        {
            case 0:
            if (searchRoot.tracks != null){
                SpawnMejoresResultados(2); 
                for (int i = 0; i < searchRoot.tracks.items.Count; i++)
                {
                    try
                    {
                        if (searchRoot.tracks.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.tracks.items[i].name, searchRoot.tracks.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.tracks.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.tracks.items[i].name);
                        
                    }
                            
                }
                
            }

            if (searchRoot.artists != null){
                SpawnMejoresResultados(3); 
                for (int i = 0; i < searchRoot.artists.items.Count; i++)
                {
                    try
                    {
                        if (searchRoot.artists.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.artists.items[i].name, searchRoot.artists.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.artists.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.artists.items[i].name);
                        
                    }
                            
                }
                
            }

            if (searchRoot.albums != null){
                SpawnMejoresResultados(4); 
                for (int i = 0; i < searchRoot.albums.items.Count; i++)
                {
                    try
                    {
                        if (searchRoot.albums.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.albums.items[i].name, searchRoot.albums.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.albums.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.albums.items[i].name);
                        
                    }
                            
                }
                
            }

            if (searchRoot.playlists != null){
                SpawnMejoresResultados(5); 
                for (int i = 0; i < searchRoot.playlists.items.Count; i++)
                {
                    try
                    {
                        if (searchRoot.playlists.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.playlists.items[i].name, searchRoot.playlists.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.playlists.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.playlists.items[i].name);
                        
                    }
                            
                }
                
            }
            LastPosition[numEnpantalla].transform.SetAsLastSibling();
            break;




            case 2:
            if (searchRoot.tracks != null){
                for (int i = 0; i < searchRoot.tracks.items.Count; i++)
                {
                    ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.tracks.items[i].name, searchRoot.tracks.items[i].images[0].url);
                    /*
                    try
                    {
                        if (searchRoot.tracks.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.tracks.items[i].name, searchRoot.tracks.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.tracks.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.tracks.items[i].name);
                        
                    }
                    */        
                }
                
            }
            break;





            case 3:
            if (searchRoot.artists != null){
                for (int i = 0; i < searchRoot.artists.items.Count; i++)
                {
                    try
                    {
                        if (searchRoot.artists.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.artists.items[i].name, searchRoot.artists.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.artists.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.artists.items[i].name);
                        
                    }
                            
                }
                
            }
            break;
              

            case 4:
            if (searchRoot.albums != null){
                for (int i = 0; i < searchRoot.albums.items.Count; i++)
                {
                    try
                    {
                        if (searchRoot.albums.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.albums.items[i].name, searchRoot.albums.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.albums.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.albums.items[i].name);
                        
                    }
                            
                }
                
            }
            break; 


            case 5:
            if (searchRoot.playlists != null){
                for (int i = 0; i < searchRoot.playlists.items.Count; i++)
                {
                    try
                    {
                        if (searchRoot.playlists.items[i].images != null){
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.playlists.items[i].name, searchRoot.playlists.items[i].images[0].url);
                        }else{
                            ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.playlists.items[i].name);
                            }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ListOfLists[numEnpantalla][i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.playlists.items[i].name);
                        
                    }
                            
                }
                
            }
            break;     
                
            
        }

    }

    private void Callback_OnCLick_CheckForSpawn(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        SearchRoot searchRoot = (SearchRoot)_value[1];
        switch (numEnpantalla)
        {
            case 3:
            if (searchRoot.artists != null){
                for (int i = 0; i < PositionInSearch; i++)
                {

                    if (searchRoot.artists.items[i].images != null && searchRoot.artists.items[i].images.Count > 0){
                        
                        ListOfLists[numEnpantalla][PositionInSearch+i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingleWithImage(searchRoot.artists.items[i].name, searchRoot.artists.items[i].images[0].url);
                    }else{
                            
                        ListOfLists[numEnpantalla][PositionInSearch+i].GetComponent<DynamicSearchPrefabInitializer>().InitializeSingle(searchRoot.artists.items[i].name);
                    }
                
                }
            }   
                
            break;
        }

        CheckForSpawnHasEnded = true;
        PositionInSearch += MaxPrefabsinScreen;
    }













    
    public void CheckForSpawn(){
        
        if(ListOfLists[numEnpantalla].Count != 0 && CheckForSpawnHasEnded){
            
            if(Scrollbar[numEnpantalla].verticalNormalizedPosition  <= ScrollbarVerticalPos){
                
                CheckForSpawnHasEnded = false;
                DynamicPrefabSpawner(MaxPrefabsinScreen);
                SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_CheckForSpawn, "ES", MaxPrefabsinScreen, PositionInSearch);
            }
        }
        
    }
    
    public void DynamicPrefabSpawner(float prefabs){
        numEnpantalla = Descubrir.GetCurrentEscena();
        if(ListOfLists[numEnpantalla].Count == 0 ){
            CalculateMaxPrefabToCall();
        }
            for (int i = 0; i <= prefabs; i++)
            {
                SpawnPrefab(false);
            }
        LastPosition[numEnpantalla].transform.SetAsLastSibling();

    }

    private void CalculateMaxPrefabToCall(){
        if(MaxPrefabsinScreen ==0){
                  
            MaxPrefabsinScreen = (int)Mathf.Round((SpawnArea.GetComponent<RectTransform>().rect.height) / Prefabs[numEnpantalla].GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public void KillAllPrefabLists(){
        Searchbar.text = null;
        foreach (List<GameObject> ListPrefab in ListOfLists)
        {
            foreach(GameObject Prefab in ListPrefab){
                Destroy(Prefab);
            }
            ListPrefab.Clear(); 
        }
    }

    public void KillPrefablist(int scene){
        foreach (GameObject Prefab in ListOfLists[scene])
        {
            Destroy(Prefab);
        }
        ListOfLists[scene].Clear();
    }
    private void SpawnMejoresResultados(int prefab){
        
        Instance = Instantiate(Prefabs[prefab],PrefabsPosition.transform.position, Quaternion.identity);
        Instance.transform.SetParent(GameObject.Find("PF_ResultadosdeBusqueda_Container").transform);
        Instance.transform.localScale = new Vector3(1,1,1);  
        ListOfLists[0].Add(Instance);
        Instance.SetActive(false);
        ListOfLists[numEnpantalla].Add(Instance);
        
        
    }

    private void SpawnPrefab(bool IsVisible){
        switch (numEnpantalla){
            case 0:
                break;
            case 1:
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Curadores_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);  
                Instance.SetActive(IsVisible);
                ListOfLists[numEnpantalla].Add(Instance);
                break;
            case 2:
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Songs_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.SetActive(IsVisible);
                ListOfLists[numEnpantalla].Add(Instance);   
                break;
            case 3: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Artists_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);  
                Instance.SetActive(IsVisible); 
                ListOfLists[numEnpantalla].Add(Instance);
                break;
            case 4: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Albums_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.SetActive(IsVisible);
                ListOfLists[numEnpantalla].Add(Instance);  
                break;
            case 5: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Playlists_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.SetActive(IsVisible);
                ListOfLists[numEnpantalla].Add(Instance);   
                break;
            case 6: 
                Instance = Instantiate(Prefabs[numEnpantalla],PrefabsPosition.transform.position, Quaternion.identity);
                Instance.transform.SetParent(GameObject.Find("PF_Genders_Container").transform);
                Instance.transform.localScale = new Vector3(1,1,1);
                Instance.SetActive(IsVisible);
                ListOfLists[numEnpantalla].Add(Instance);
                break;
        }

    }
}
