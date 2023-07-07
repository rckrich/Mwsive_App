using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GG.Infrastructure.Utils.Swipe;

public class SurfManager : Manager
{
    public SwipeListener swipeListener;
    public ScrollRect Controller;
    public GameObject Prefab;
    public List <GameObject> MwsiveSongs = new List<GameObject>();
    public GameObject[ ] RestPositions;

    public float MaxRotation = 18f;
    public float SurfSuccessSensitivity = 2.2f;



    private Vector2 ControllerPostion = new Vector2();
    public int CurrentPosition = 0;
    private int PrefabPosition = 0;
    

  
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
                SideScrollSuccess();
                Controller.vertical =false;
                Controller.horizontal =false;
            break;
            case "Up":
                UpScrollSuccess();
                Controller.vertical =false;
                Controller.horizontal =false;
            break;
            case "Down":
                DownScrollSuccess();
                Controller.vertical =false;
                Controller.horizontal =false;
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

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], var);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], var);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], var);
            
    }


    private void UpScrollAnimation(){
        
            float var = Controller.transform.position.y/ControllerPostion.y;
            float Fade =ControllerPostion.y/Controller.transform.position.y;

            UIAniManager.instance.SurfVerticalUp(MwsiveSongs[CurrentPosition],var*.5f, MaxRotation, Fade,false);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], var*.25f);

           
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
        
        if(MwsiveSongs[CurrentPosition].transform.position.x >= ControllerPostion.x*SurfSuccessSensitivity){
            SideScrollSuccess();

        }else if(MwsiveSongs[CurrentPosition].transform.position.y >= ControllerPostion.y*SurfSuccessSensitivity){
            UpScrollSuccess();

        }else if(MwsiveSongs[CurrentPosition].transform.position.y <= ControllerPostion.y/SurfSuccessSensitivity){
            DownScrollSuccess();

        }else{
            ResetValue();
        }
    }

    private void SideScrollSuccess(){
        Controller.horizontal =true;
        Controller.vertical =true;
        Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
        UIAniManager.instance.SurfSide(MwsiveSongs[CurrentPosition],1, -MaxRotation,0,true);

        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], 1);
        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], 1);
        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], 1);
        CurrentPosition++;
        SpawnPrefab();

        Debug.Log("SideScrollSuccess");
    }
    private void UpScrollSuccess(){
        Controller.horizontal =true;
        Controller.vertical =true;
        Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
        UIAniManager.instance.SurfVerticalUp(MwsiveSongs[CurrentPosition],1, MaxRotation, 0,true);


        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], 1);
        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], 1);
        UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], 1);
        CurrentPosition++;
        SpawnPrefab();

        Debug.Log("UpScrollSuccess");
    }
    private void DownScrollSuccess(){
            Controller.horizontal =true;
            Controller.vertical =true;
            Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
            UIAniManager.instance.SurfVerticalDown(MwsiveSongs[CurrentPosition],1, -MaxRotation, 0,true);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+1], RestPositions[0], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+2], RestPositions[1], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition+3], RestPositions[2], 1);
            CurrentPosition++;
            SpawnPrefab();

            Debug.Log("DownScrollSuccess");
    }
           


    public void ResetValue(){
        Controller.horizontal =true;
        Controller.vertical =true;
        Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
        UIAniManager.instance.SurfReset(MwsiveSongs[CurrentPosition]);
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
            
            if(PrefabPosition < 1){
                Instance.transform.localScale = new Vector3 (.9f,.9f,.9f);
            }else if (PrefabPosition < 2){
                Instance.transform.localScale = new Vector3 (.8f,.8f,.8f);
            }else if (PrefabPosition < 3){
                Instance.transform.localScale = new Vector3 (.7f,.7f,.7f);
            }else if (PrefabPosition < 4){
                Instance.transform.localScale = new Vector3 (.6f,.6f,.6f);
            }
            

        }else{
            Instance = Instantiate(Prefab,RestPositions[3].transform.position, Quaternion.identity);
            Instance.SetActive(false);
            Instance.GetComponent<CanvasGroup>().alpha = 0;
            Instance.transform.localScale = new Vector3 (.6f,.6f,.6f);
            
            
        }
        Instance.name = "PF_Mwsive_Song " + PrefabPosition;
        Instance.GetComponent<RectTransform>().sizeDelta = new Vector2 (100, 100);
        Instance.transform.SetParent(GameObject.Find("PF_Mwsive_Container").transform);
        Instance.transform.SetAsFirstSibling();
        MwsiveSongs.Add(Instance);
        PrefabPosition++;

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

}
