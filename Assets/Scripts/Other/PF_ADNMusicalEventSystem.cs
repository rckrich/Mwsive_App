using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PF_ADNMusicalEventSystem : MonoBehaviour
{

    public string[] types = new string[] {"artist"};

    public GameObject DynamicScroll, PlaceHolder;
    private string SearchText;
    public  TMP_InputField searchbar;
    public TextMeshProUGUI Number;
    public int MaxNumerofPrefabsInstanciate;
    private bool EnableSerach = false;
    private int PositionInSearch = 0;


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)){
            EnableSerach = true;
            Search();
        }else{
            EnableSerach = false;
        }
    }

    


    private void OnEnable() {
        
    }
    public void ChangeName(int num ){
        Number.text = "#" +num;
        
    }
    

    public void Search(){
        PlaceHolder.SetActive(false);
        SearchText = searchbar.text;
        DynamicScroll.GetComponent<DynamicScroll>().MaxPrefabsInScreen = MaxNumerofPrefabsInstanciate; 
        
        ADNDynamicScroll.instance.HideAllOtherInstances(gameObject.name);
        if(SearchText.Length >= 3 || EnableSerach){
            
            DynamicScroll.transform.DOScaleY(1, 0.5F).OnComplete(() => {StartCoroutine(UpdateLayoutGroup());    
            });
            
            if (DynamicScroll.GetComponent<DynamicScroll>().Instances.Count ==0){
                SpotifyConnectionManager.instance.SearchForItem(SearchText, types, Callback_OnCLick_SearchForItem, "ES", MaxNumerofPrefabsInstanciate);
                DynamicScroll.GetComponent<DynamicScroll>().DynamicPrefabSpawner(DynamicScroll.GetComponent<DynamicScroll>().MaxPrefabsInScreen);
                
            }
            
        }
    }
    IEnumerator UpdateLayoutGroup()
    {
        gameObject.GetComponent<VerticalLayoutGroup>().enabled= false;
    
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<VerticalLayoutGroup>().enabled= true;
    }

    public void End(){
        Debug.Log("eND");
        
        DynamicScroll.transform.DOScaleY(0, 0.5F);
        DynamicScroll.GetComponent<DynamicScroll>().KillPrefablist();
        
        
    }

    

    private void Callback_OnCLick_SearchForItem(object[] _value)
    {
        if (SpotifyConnectionManager.instance.CheckReauthenticateUser((long)_value[0])) return;

        SearchRoot searchRoot = (SearchRoot)_value[1];
        
        List<GameObject> _Instances = DynamicScroll.GetComponent<DynamicScroll>().GetInstances();
        if (searchRoot.artists != null){
            for (int i = 0; i < searchRoot.artists.items.Count-1; i++)
            {
                if (searchRoot.artists.items[i].images != null && searchRoot.artists.items[i].images.Count > 0){
                    _Instances[i].GetComponent<ADNMusicalPrefabInitializaer>().InitializeSingleWithBackgroundWithImage(searchRoot.artists.items[i].name, searchRoot.artists.items[i].images[0].url);
                
                }else{
                    _Instances[i].GetComponent<ADNMusicalPrefabInitializaer>().InitializeSingleWithBackgroundNoImage(searchRoot.artists.items[i].name);
                }
                
            }
        }
        



    }

}
