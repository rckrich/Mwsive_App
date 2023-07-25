using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PF_ADNMusicalEventSystem : MonoBehaviour
{
    public GameObject DynamicScroll, PlaceHolder;
    private string SearchText;
    public  TMP_InputField searchbar;
    public TextMeshProUGUI Number;
    private bool EnableSerach = false;

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
        DynamicScroll.GetComponent<DynamicScroll>().MaxPrefabsInScreen = 3; 
        
        ADNDynamicScroll.instance.HideAllOtherInstances(gameObject.name);
        if(SearchText.Length >= 3 || EnableSerach){
            
            DynamicScroll.transform.DOScaleY(1, 0.5F);
            ADNDynamicScroll.instance.Rebuild();
            if (DynamicScroll.GetComponent<DynamicScroll>().Instances.Count ==0){
                
                DynamicScroll.GetComponent<DynamicScroll>().DynamicPrefabSpawner(DynamicScroll.GetComponent<DynamicScroll>().MaxPrefabsInScreen);
                
            }
            foreach (GameObject item in DynamicScroll.GetComponent<DynamicScroll>().GetInstances())
            {
                item.GetComponent<ADNMusicalPrefabInitializaer>().InitializeSingleWithBackground(SearchText);
            }
        }
    }
    public void End(){
        Debug.Log("eND");
        
        DynamicScroll.transform.DOScaleY(0, 0.5F);
        DynamicScroll.GetComponent<DynamicScroll>().KillPrefablist();
        
        
    }
}
