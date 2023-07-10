using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GG.Infrastructure.Utils.Swipe;

public class SurfManager : Manager
{
    public SwipeListener swipeListener;
    public ScrollRect Controller;
    public GameObject Prefab;
    public GameObject AddSong;
    public List <GameObject> MwsiveSongs = new List<GameObject>();
    public GameObject[ ] RestPositions;

    public float MaxRotation = 18f;
    public float SurfSuccessSensitivity = 2.2f;
    public Vector2 LeftRightOffset;



    private Vector2 ControllerPostion = new Vector2();
    public int CurrentPosition = 0;
    public int PrefabPosition = 0;
    public bool HasSwipeEnded = true;
    
  
    private void Start() {
        ControllerPostion = new Vector2(Controller.transform.position.x, Controller.transform.position.y); 
        SpawnPrefab();
        SpawnPrefab();
        SpawnPrefab();
        SpawnPrefab();
        SpawnPrefab();

        
    }
    private void OnEnable() {
        swipeListener.OnSwipe.AddListener(OnSwipe);
        foreach(GameObject song in GameObject.FindGameObjectsWithTag("MwsiveSong")){
            MwsiveSongs.Add(song);
        }
    }

    private void OnSwipe(string swipe){
        switch (swipe){
            case "Right":
                
                Controller.vertical =false;
                Controller.horizontal =false;
                HasSwipeEnded = false;
                SideScrollSuccess();
            break;
            case "Up":
                
                Controller.vertical =false;
                Controller.horizontal =false;
                HasSwipeEnded = false;
                UpScrollSuccess();
            break;
            case "Down":
                
                Controller.vertical =false;
                Controller.horizontal =false;
                HasSwipeEnded = false;
                DownScrollSuccess();
            break;
        }
        Debug.Log(swipe);
    }
    private void OnDisable() {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    
    public void ValChange(){

        if(Controller.transform.position.x > ControllerPostion.x*1.1){
            Controller.vertical =false;
           SideScrollAnimation();
        }if(Controller.transform.position.y > ControllerPostion.y*1.1){
            Controller.horizontal =false;
            UpScrollAnimation();
        }if(Controller.transform.position.y < ControllerPostion.y*.9){
            Controller.horizontal =false;
            DownScrollAnimation();
        }

    }



    private void SideScrollAnimation(){
         
            
            float var = Controller.transform.position.x/ControllerPostion.x*.25f;
            float Fade =ControllerPostion.x/Controller.transform.position.x;

            UIAniManager.instance.SurfSide(MwsiveSongs[CurrentPosition],var, -MaxRotation, Fade,false);
            UIAniManager.instance.SurfAddSong(AddSong, var);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], var);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], var);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], var);
            
    }


    private void UpScrollAnimation(){
        
            float var = Controller.transform.position.y/ControllerPostion.y;
            float Fade =ControllerPostion.y/Controller.transform.position.y;

            UIAniManager.instance.SurfVerticalUp(MwsiveSongs[CurrentPosition],var*.5f, MaxRotation, Fade,false);

            
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[1], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[2], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[3], var*.25f);
            

           
    }


    private void DownScrollAnimation(){
        //DownScroll
            
            float var = Controller.transform.position.y/ControllerPostion.y;
            float Fade = Controller.transform.position.y/ControllerPostion.y;
            float VAR2  =ControllerPostion.y/Controller.transform.position.y;
            

            UIAniManager.instance.SurfVerticalDown(MwsiveSongs[CurrentPosition],var*-.5f, MaxRotation, Fade,false);

            

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], VAR2*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], VAR2*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], VAR2*.25f);
            
            
    }

    public void OnEndDrag(){
        while (HasSwipeEnded){
            if(MwsiveSongs[CurrentPosition].transform.position.x >= ControllerPostion.x*SurfSuccessSensitivity){
                SideScrollSuccess();
                break;

            }else if(MwsiveSongs[CurrentPosition].transform.position.y >= ControllerPostion.y*SurfSuccessSensitivity){
                UpScrollSuccess();
                break;

            }else if(MwsiveSongs[CurrentPosition].transform.position.y <= ControllerPostion.y/SurfSuccessSensitivity){
                DownScrollSuccess();
                break;

            }else{
                ResetValue();
                break;
            }
        }
        Controller.enabled =true;    
        
        
    }

    private void SideScrollSuccess(){
        Controller.enabled =false;
        Controller.horizontal =true;
        Controller.vertical =true;
        Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
        UIAniManager.instance.SurfSide(MwsiveSongs[CurrentPosition],1, -MaxRotation,0,true);
        UIAniManager.instance.CompleteSurfAddSong(AddSong, 1.5f);

        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], 1);
        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], 1);
        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], 1);
        CurrentPosition++;
        if(CurrentPosition == PrefabPosition-3){
            SpawnPrefab();
        }
        
        HasSwipeEnded = true;
        Debug.Log("SideScrollSuccess");
    }
    private void UpScrollSuccess(){
        Controller.enabled =false;
        Controller.horizontal =true;
        Controller.vertical =true;
        Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
        if(CurrentPosition > 0){
            Debug.Log(DOTween.KillAll(MwsiveSongs[CurrentPosition-1]));
            UIAniManager.instance.SurfVerticalUp(MwsiveSongs[CurrentPosition],1, MaxRotation, 0,false);
            
            UIAniManager.instance.SurfTransitionBackSong(MwsiveSongs[CurrentPosition-1], RestPositions[0]);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition], RestPositions[1], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[2], 1);
            UIAniManager.instance.SurfTransitionBackHideSong(MwsiveSongs[CurrentPosition+2], RestPositions[3], 1);
            CurrentPosition--;
            
        }else{
            ResetValue();
        }

        
        HasSwipeEnded = true;
        Debug.Log("UpScrollSuccess");
    }
    private void DownScrollSuccess(){
            Controller.enabled =false;
            Controller.horizontal =true;
            Controller.vertical =true;
            Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
            UIAniManager.instance.SurfVerticalDown(MwsiveSongs[CurrentPosition],1, -MaxRotation, 0,true);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], 1);
            CurrentPosition++;
            if(CurrentPosition == PrefabPosition -3){
                SpawnPrefab();
            }

            HasSwipeEnded = true;
            Debug.Log("DownScrollSuccess");
    }
           


    public void ResetValue(){
        Debug.Log("Reset");
        Controller.horizontal =true;
        Controller.vertical =true;
        HasSwipeEnded = true;
        Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
        UIAniManager.instance.SurfReset(MwsiveSongs[CurrentPosition]);
        UIAniManager.instance.SurfAddSongReset(AddSong);
        
        UIAniManager.instance.SurfResetOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[1], true);
        UIAniManager.instance.SurfResetOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[2], true);
        UIAniManager.instance.SurfResetOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[3], false);

       // UIAniManager.instance.SurfAddSongReset(); 
    }

    public GameObject GetCurrentPrefab(){
        GameObject _Instance = MwsiveSongs[CurrentPosition];
        return _Instance;
    }

    private void SpawnPrefab(){
        GameObject Instance;
        if(PrefabPosition < 4){
            Instance = Instantiate(Prefab,RestPositions[PrefabPosition].transform.position, Quaternion.identity);
            Instance.SetActive(true);
            Instance.GetComponent<CanvasGroup>().alpha = RestPositions[PrefabPosition].GetComponent<CanvasGroup>().alpha;

        }else{
            Instance = Instantiate(Prefab,RestPositions[3].transform.position, Quaternion.identity);
            Instance.SetActive(false);
            Instance.GetComponent<CanvasGroup>().alpha = 0;
            
        }
        Instance.name = "PF_Mwsive_Song " + PrefabPosition;
        
        Instance.transform.SetParent(GameObject.Find("PF_Mwsive_Container").transform);
        Instance.transform.SetAsFirstSibling();
        MwsiveSongs.Add(Instance);
        Instance.GetComponent<RectTransform>().offsetMin = new Vector2 (LeftRightOffset.x,0);
        Instance.GetComponent<RectTransform>().offsetMax = new Vector2 (LeftRightOffset.y,0);
        if(PrefabPosition < 1){
                Instance.transform.localScale = new Vector3 (1f,1f,1f);
                Instance.transform.position = RestPositions[PrefabPosition].transform.position;
            }else if (PrefabPosition < 2){
                Instance.transform.localScale = new Vector3 (.9f,.9f,.9f);
                Instance.transform.position = RestPositions[PrefabPosition].transform.position;
            }else if (PrefabPosition < 3){
                Instance.transform.localScale = new Vector3 (.8f,.8f,.8f);
                Instance.transform.position = RestPositions[PrefabPosition].transform.position;
            }else if (PrefabPosition >3){
                Instance.transform.localScale = new Vector3 (.6f,.6f,.6f);
                Instance.transform.position = RestPositions[3].transform.position;
            }
        
        PrefabPosition++;
    }


}
