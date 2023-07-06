using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GG.Infrastructure.Utils.Swipe;

public class SurfManager : Manager
{
    //public SwipeListener swipeListener;
    public ScrollRect Controller;
    public GameObject Prefab;
    public List <GameObject> MwsiveSongs = new List<GameObject>();
    public GameObject[ ] RestPositions;

    public float MaxRotation = 18f;
    public float SurfSuccessSensitivity = 2.2f;



    private Vector2 ControllerPostion = new Vector2();
    public int CurrentPosition;

  
    private void Start() {
        ControllerPostion = new Vector2(Controller.transform.position.x, Controller.transform.position.y); 
        
    }
    private void OnEnable() {
        //swipeListener.OnSwipe.AddListener(OnSwipe);
        foreach(GameObject song in GameObject.FindGameObjectsWithTag("MwsiveSong")){
            MwsiveSongs.Add(song);
        }
        CurrentPosition = MwsiveSongs.Count-1;
    }
/*
    private void OnSwipe(string swipe){
        switch (swipe){
            case "Right":
                UIAniManager.instance.SideTransitionExitCenter(Prefab);
            break;
            case "Up":
                UIAniManager.instance.VerticalFadeTransitionExitCenter(Prefab, false);
            break;
            case "Down":
                UIAniManager.instance.VerticalFadeTransitionExitCenter(Prefab, true);
            break;
        }
        Debug.Log(swipe);
    }
    private void OnDisable() {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
*/
    
    public void ValChange(){
        if(Controller.transform.position.x > ControllerPostion.x*1.1){
           SideScroll();
        }if(Controller.transform.position.y > ControllerPostion.y*1.1){
            UpScroll();
        }if(Controller.transform.position.y < ControllerPostion.y*.9){
            DownScroll();
        }

    }



    private void SideScroll(){
         //SideScroll
            
            float var = Controller.transform.position.x/ControllerPostion.x*.25f;
            float Fade =ControllerPostion.x/Controller.transform.position.x;

            UIAniManager.instance.SurfSide(MwsiveSongs[CurrentPosition],var, -MaxRotation, Fade,false);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-1], RestPositions[0], var);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-2], RestPositions[1], var);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-3], RestPositions[2], var);
            
    }


    private void UpScroll(){
        //UpScroll
            float var = Controller.transform.position.y/ControllerPostion.y;
            float Fade =ControllerPostion.y/Controller.transform.position.y;

            UIAniManager.instance.SurfVerticalUp(MwsiveSongs[CurrentPosition],var*.5f, MaxRotation, Fade,false);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-1], RestPositions[0], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-2], RestPositions[1], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-3], RestPositions[2], var*.25f);

           
    }


    private void DownScroll(){
        //DownScroll
            float var = ControllerPostion.y/Controller.transform.position.y;
            float Fade = Controller.transform.position.y/ControllerPostion.y;

            UIAniManager.instance.SurfVerticalDown(MwsiveSongs[CurrentPosition],var*.05f, -MaxRotation, Fade,false);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-1], RestPositions[0], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-2], RestPositions[1], var*.25f);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-3], RestPositions[2], var*.25f);
            
            
    }

    public void OnEndDrag(){
        
        if(MwsiveSongs[CurrentPosition].transform.position.x >= ControllerPostion.x*SurfSuccessSensitivity){
            UIAniManager.instance.SurfSide(MwsiveSongs[CurrentPosition],1, -MaxRotation,0,true);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-1], RestPositions[0], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-2], RestPositions[1], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-3], RestPositions[2], 1);
            CurrentPosition++;
            SpawnPrefab();

            Debug.Log("SideScrollSuccess");
        }else if(MwsiveSongs[CurrentPosition].transform.position.y >= ControllerPostion.y*SurfSuccessSensitivity){
            UIAniManager.instance.SurfVerticalUp(MwsiveSongs[CurrentPosition],1, MaxRotation, 0,true);


            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-1], RestPositions[0], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-2], RestPositions[1], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-3], RestPositions[2], 1);
            CurrentPosition++;
            SpawnPrefab();

            Debug.Log("UpScrollSuccess");
        }else if(MwsiveSongs[CurrentPosition].transform.position.y <= ControllerPostion.y/SurfSuccessSensitivity){

            UIAniManager.instance.SurfVerticalDown(MwsiveSongs[CurrentPosition],1, -MaxRotation, 0,true);

            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-1], RestPositions[0], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-2], RestPositions[1], 1);
            UIAniManager.instance.SurfTransitionOtherSongs(MwsiveSongs[CurrentPosition-3], RestPositions[2], 1);
            CurrentPosition++;

            Debug.Log("DownScrollSuccess");
        }else{
            ResetValue();
        }
    }
           


    public void ResetValue(){
        Controller.transform.position = new Vector2(ControllerPostion.x,ControllerPostion.y);
        UIAniManager.instance.SurfReset(MwsiveSongs[CurrentPosition]);
            UIAniManager.instance.SurfResetOtherSongs(MwsiveSongs[CurrentPosition-1], RestPositions[1], true);
            UIAniManager.instance.SurfResetOtherSongs(MwsiveSongs[CurrentPosition-2], RestPositions[2], true);
            UIAniManager.instance.SurfResetOtherSongs(MwsiveSongs[CurrentPosition-3], RestPositions[3], false);
    }

    private void SpawnPrefab(){
        GameObject Instance;
        Instance = Instantiate(Prefab,RestPositions[3].transform.position, Quaternion.identity);
        Instance.SetActive(false);
        Instance.transform.parent = GameObject.Find("PF_Mwsive_Container").transform;
        Instance.transform.SetAsFirstSibling();
        MwsiveSongs.Add(Instance);

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

}
