using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIAniManager : MonoBehaviour
{
    public GameObject MainCanvas;
    public float MoveTransitionDuration =0.5f;
    public float ScaleTransitionDuration =1f;
    public float FadeTransitionDuration = 1f;
    public float ColorTransitionDuration = 0.5f;
    public enum AnimationTypeCurves {Flash, InBack, InBounce, InCirc, InCubic, InElastic, InExpo, InFlash, InOutBack, InOutBounc, InOutCirc, InOutCubic, InOutExpo, InOutFlash, InOutQuad, InOutQuart, InOutQuint, InOutSine, InQuad, InQuint, InSine, Linear, OutBack, OutBounce, OutCirc, OutCubic, OutElastic, OutExpo, OutFlash, OutQuad, OutQuart, OutQuint, OutSine, UnSet}
    public AnimationTypeCurves AnimationMove;
    public AnimationTypeCurves AnimationScale;
    public AnimationTypeCurves AnimationFade;


    private static UIAniManager _instance;
    private Vector2 FinalPosition, RestPositionSide, RestPositionDown, RestPositionUp; 
    private Ease _AnimationMove;
    private Ease _AnimationScale;
    private Ease _AnimationFade;
    private bool ChangeColorIsOn = true;

    public static UIAniManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIAniManager>();
            }
            return _instance;
        }
    }

    void Start()
    {
        SetPosition();
        switch(AnimationMove){
            case AnimationTypeCurves.Flash:
                _AnimationMove = Ease.Flash;
                break;
            case AnimationTypeCurves.InBack:
                _AnimationMove = Ease.InBack;
                break;
            case AnimationTypeCurves.InBounce:
                _AnimationMove = Ease.InBounce;
                break;
            case AnimationTypeCurves.InCirc:
                _AnimationMove = Ease.InCirc;
                break;
            case AnimationTypeCurves.InCubic:
                _AnimationMove = Ease.InCubic;
                break;
            case AnimationTypeCurves.InElastic:
                _AnimationMove = Ease.InElastic;
                break;
            case AnimationTypeCurves.InExpo:
                _AnimationMove = Ease.InExpo;
                break;
            case AnimationTypeCurves.InFlash:
                _AnimationMove = Ease.InFlash;
                break;
            case AnimationTypeCurves.InOutBack:
                _AnimationMove = Ease.InOutBack;
                break;
            case AnimationTypeCurves.InOutBounc:
                _AnimationMove = Ease.InOutBounce;
                break;
            case AnimationTypeCurves.InOutCirc:
                _AnimationMove = Ease.InOutCirc;
                break;
            case AnimationTypeCurves.InOutCubic:
                _AnimationMove = Ease.InOutCubic;
                break;
            case AnimationTypeCurves.InOutExpo:
                _AnimationMove = Ease.InOutExpo;
                break;
            case AnimationTypeCurves.InOutFlash:
                _AnimationMove = Ease.InOutFlash;
                break;
            case AnimationTypeCurves.InOutQuad:
                _AnimationMove = Ease.InOutQuad;
                break;
            case AnimationTypeCurves.InOutQuart:
                _AnimationMove = Ease.InOutQuart;
                break;
            case AnimationTypeCurves.OutQuint:
                _AnimationMove = Ease.OutQuint;
                break;
            case AnimationTypeCurves.OutSine:
                _AnimationMove = Ease.OutSine;
                break;
            case AnimationTypeCurves.UnSet:
                _AnimationMove = Ease.Unset;
                break;
            default:
                _AnimationMove = Ease.Unset;
                break;
            
        }
        switch(AnimationScale){
            case AnimationTypeCurves.Flash:
                _AnimationScale = Ease.Flash;
                break;
            case AnimationTypeCurves.InBack:
                _AnimationScale = Ease.InBack;
                break;
            case AnimationTypeCurves.InBounce:
                _AnimationScale = Ease.InBounce;
                break;
            case AnimationTypeCurves.InCirc:
                _AnimationScale = Ease.InCirc;
                break;
            case AnimationTypeCurves.InCubic:
                _AnimationScale = Ease.InCubic;
                break;
            case AnimationTypeCurves.InElastic:
                _AnimationScale = Ease.InElastic;
                break;
            case AnimationTypeCurves.InExpo:
                _AnimationScale = Ease.InExpo;
                break;
            case AnimationTypeCurves.InFlash:
                _AnimationScale = Ease.InFlash;
                break;
            case AnimationTypeCurves.InOutBack:
                _AnimationScale = Ease.InOutBack;
                break;
            case AnimationTypeCurves.InOutBounc:
                _AnimationScale = Ease.InOutBounce;
                break;
            case AnimationTypeCurves.InOutCirc:
                _AnimationScale = Ease.InOutCirc;
                break;
            case AnimationTypeCurves.InOutCubic:
                _AnimationScale = Ease.InOutCubic;
                break;
            case AnimationTypeCurves.InOutExpo:
                _AnimationScale = Ease.InOutExpo;
                break;
            case AnimationTypeCurves.InOutFlash:
                _AnimationScale = Ease.InOutFlash;
                break;
            case AnimationTypeCurves.InOutQuad:
                _AnimationScale = Ease.InOutQuad;
                break;
            case AnimationTypeCurves.InOutQuart:
                _AnimationScale = Ease.InOutQuart;
                break;
            case AnimationTypeCurves.OutQuint:
                _AnimationScale = Ease.OutQuint;
                break;
            case AnimationTypeCurves.OutSine:
                _AnimationScale = Ease.OutSine;
                break;
            case AnimationTypeCurves.UnSet:
                _AnimationScale = Ease.Unset;
                break;
            default:
                _AnimationScale = Ease.Unset;
                break;
            
        }
        switch(AnimationFade){
            case AnimationTypeCurves.Flash:
                _AnimationFade = Ease.Flash;
                break;
            case AnimationTypeCurves.InBack:
                _AnimationFade = Ease.InBack;
                break;
            case AnimationTypeCurves.InBounce:
                _AnimationFade = Ease.InBounce;
                break;
            case AnimationTypeCurves.InCirc:
                _AnimationFade = Ease.InCirc;
                break;
            case AnimationTypeCurves.InCubic:
                _AnimationFade = Ease.InCubic;
                break;
            case AnimationTypeCurves.InElastic:
                _AnimationFade = Ease.InElastic;
                break;
            case AnimationTypeCurves.InExpo:
                _AnimationFade = Ease.InExpo;
                break;
            case AnimationTypeCurves.InFlash:
                _AnimationFade = Ease.InFlash;
                break;
            case AnimationTypeCurves.InOutBack:
                _AnimationFade = Ease.InOutBack;
                break;
            case AnimationTypeCurves.InOutBounc:
                _AnimationFade = Ease.InOutBounce;
                break;
            case AnimationTypeCurves.InOutCirc:
                _AnimationFade = Ease.InOutCirc;
                break;
            case AnimationTypeCurves.InOutCubic:
                _AnimationFade = Ease.InOutCubic;
                break;
            case AnimationTypeCurves.InOutExpo:
                _AnimationFade = Ease.InOutExpo;
                break;
            case AnimationTypeCurves.InOutFlash:
                _AnimationFade = Ease.InOutFlash;
                break;
            case AnimationTypeCurves.InOutQuad:
                _AnimationFade = Ease.InOutQuad;
                break;
            case AnimationTypeCurves.InOutQuart:
                _AnimationFade = Ease.InOutQuart;
                break;
            case AnimationTypeCurves.OutQuint:
                _AnimationFade = Ease.OutQuint;
                break;
            case AnimationTypeCurves.OutSine:
                _AnimationFade = Ease.OutSine;
                break;
            case AnimationTypeCurves.UnSet:
                _AnimationFade = Ease.Unset;
                break;
            default:
                _AnimationFade = Ease.Unset;
                break;
            
        }
    }

    void SetPosition(){
        FinalPosition = MainCanvas.transform.position;
        RestPositionDown = new Vector2(MainCanvas.transform.position.x, -MainCanvas.transform.position.y);
        RestPositionUp = new Vector2(MainCanvas.transform.position.x, 2*MainCanvas.transform.position.y);
        RestPositionSide = new Vector2(MainCanvas.transform.position.x*4, MainCanvas.transform.position.y);
    }

    void SetPosition(GameObject GA){
        FinalPosition = GA.transform.position;
        RestPositionDown = new Vector2(GA.transform.position.x, -GA.transform.position.y);
        RestPositionUp = new Vector2(GA.transform.position.x, GA.transform.position.y*2);
        RestPositionSide = new Vector2(GA.transform.position.x*4, GA.transform.position.y);
    }

    void SetPosition(GameObject GA, float OffSet){
        FinalPosition = GA.transform.position;
        RestPositionDown = new Vector2(GA.transform.position.x, GA.transform.position.y-OffSet);
        RestPositionUp = new Vector2(GA.transform.position.x, GA.transform.position.y+OffSet);
        RestPositionSide = new Vector2(GA.transform.position.x+OffSet, GA.transform.position.y);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeColorRainbow(GameObject GA){
        if(ChangeColorIsOn){
            GA.GetComponent<Image>().DOColor(Random.ColorHSV(), ColorTransitionDuration).OnComplete(() => {ChangeColorRainbow(GA);});
        }else{
            GA.GetComponent<Image>().DOColor(Color.white, ColorTransitionDuration);
            ChangeColorIsOn = true;
        }


 
        //GA.GetComponent<Material>().DOColor(Random.ColorHSV(), ColorTransitionDuration).OnComplete(() => {ChangeColorRainbow(GA);});
    }

    public void TestEnter(GameObject GA){
        SideTransitionEnterCustomLocation(GA, 1000F);
    }
    public void TestExit(GameObject GA){
        SideTransitionExitCustomLocation(GA, 1000F);
    }

    public void KillChangeColor(){
        ChangeColorIsOn = false;
    }

    public void SideTransitionExitCenter(GameObject GA){
        SetPosition();
        GA.transform.DOMove(RestPositionSide, MoveTransitionDuration, false).OnComplete(() => {GA.SetActive(false);}).SetEase(_AnimationMove);
    }

    public void SideTransitionExitCustomLocation(GameObject GA){
        SetPosition(GA);
        GA.transform.DOMove(RestPositionSide, MoveTransitionDuration, false).OnComplete(() => {GA.SetActive(false); GA.transform.position = FinalPosition;}).SetEase(_AnimationMove);
    }

    public void SideTransitionExitCustomLocation(GameObject GA, float OffSet){
        SetPosition(GA, OffSet);
        GA.transform.DOMove(RestPositionSide, MoveTransitionDuration, false).OnComplete(() => {GA.SetActive(false); GA.transform.position = FinalPosition;}).SetEase(_AnimationMove);
    }

    public void SideTransitionEnterCustomLocation(GameObject GA){
        SetPosition(GA);
        GA.transform.position = RestPositionSide;
        GA.SetActive(true);
        GA.transform.DOMove(FinalPosition, MoveTransitionDuration, false).SetEase(_AnimationMove);
    }

    public void SideTransitionEnterCustomLocation(GameObject GA, float OffSet){
        SetPosition(GA, OffSet);
        GA.transform.position = RestPositionSide;
        GA.SetActive(true);
        GA.transform.DOMove(FinalPosition, MoveTransitionDuration, false).SetEase(_AnimationMove);
    }

    public void SideTransitionEnterCenter(GameObject GA){
        SetPosition();
        GA.transform.position = RestPositionSide;
        GA.SetActive(true);
        GA.transform.DOMove(FinalPosition, MoveTransitionDuration, false).SetEase(_AnimationMove);
    }

    public void VerticalFadeTransitionEnterCenter(GameObject GA){
        GA.GetComponent<CanvasGroup>().alpha = 0;
        SetPosition();
        GA.transform.position = RestPositionDown;
        GA.SetActive(true);
        GA.GetComponent<CanvasGroup>().DOFade(1f, FadeTransitionDuration).SetEase(_AnimationFade);
        GA.transform.DOMove(FinalPosition, MoveTransitionDuration, false).SetEase(_AnimationMove);
    }

    public void VerticalFadeTransitionExitCenter(GameObject GA, bool Down){
        SetPosition();
        GA.GetComponent<CanvasGroup>().DOFade(0f, FadeTransitionDuration* 0.2f);
        if(Down){
            GA.transform.DOMove(RestPositionDown, MoveTransitionDuration, false).OnComplete(() => {GA.SetActive(false);}).SetEase(_AnimationFade);
        }else{
            GA.transform.DOMove(RestPositionUp, MoveTransitionDuration, false).OnComplete(() => {GA.SetActive(false);}).SetEase(_AnimationFade);
        }
        
        
    }

    public void VerticalTransitionEnterCenter(GameObject GA){
        SetPosition();
        GA.transform.position = RestPositionDown;
        GA.SetActive(true);
        GA.transform.DOMove(FinalPosition, MoveTransitionDuration, false).SetEase(_AnimationMove);
    }

    public void VerticalTransitionExitCenter(GameObject GA){
        SetPosition();
        GA.transform.DOMove(RestPositionDown, MoveTransitionDuration, false).OnComplete(() => {GA.SetActive(false);}).SetEase(_AnimationMove);
        
    }

    public void PopUpScaleEnter(GameObject GA){
        SetPosition();
        GA.transform.position = FinalPosition;
        GA.SetActive(true);
        
        GA.transform.DOScale(new Vector3(1,1,1), ScaleTransitionDuration).SetEase(_AnimationScale);
    }

    public void PopUpScaleExit(GameObject GA){
        SetPosition();
        GA.transform.DOScale(new Vector3(0,0,0), ScaleTransitionDuration).OnComplete(() => {GA.SetActive(false);}).SetEase(_AnimationScale);

    }

    

}
