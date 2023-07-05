using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class SurfManager : Manager
{
    public SwipeListener swipeListener;
    public GameObject Prefab;

    private Vector2 MousePosition = new Vector2();

    // Start is called before the first frame update
    private void OnEnable() {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

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

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)){
            UpdateMousePosition();
        }
    }
    private void UpdateMousePosition(){
        MousePosition.x = Input.mousePosition.x;
        MousePosition.y = Input.mousePosition.y;
    }

}
