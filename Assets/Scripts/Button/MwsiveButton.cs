using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MwsiveButton : MonoBehaviour
{
    public GameObject OlaColorButton;
    private bool IsItOlaColorButtonActive = false;
    public GameObject AñadirColorButton;
    private bool IsItAñadirColorButtonActive = false;
    public GameObject CompartirColorButton;
    private bool IsItCompartirColorButtonActive = false;

    public void OnClickOlaButton(float AnimationDuration){
        if(!IsItOlaColorButtonActive){
            UIAniManager.instance.FadeIn(OlaColorButton, AnimationDuration);
            IsItOlaColorButtonActive = true;
        }else{
            UIAniManager.instance.FadeOut(OlaColorButton, AnimationDuration);
            IsItOlaColorButtonActive = false;
        }  
    }

    public bool GetIsItOlaActive(){
        return IsItOlaColorButtonActive;
    }

    public void OnClickAñadirButton(float AnimationDuration){
        if(!IsItAñadirColorButtonActive){
            UIAniManager.instance.FadeIn(AñadirColorButton, AnimationDuration);
            IsItAñadirColorButtonActive = true;
        }else{
            UIAniManager.instance.FadeOut(AñadirColorButton, AnimationDuration);
            IsItAñadirColorButtonActive = false;
        }  
    }


    public void OnClickCompartirButton(float AnimationDuration){
        if(!IsItCompartirColorButtonActive){
            UIAniManager.instance.FadeIn(CompartirColorButton, AnimationDuration);
            IsItCompartirColorButtonActive = true;
        }else{
            UIAniManager.instance.FadeOut(CompartirColorButton, AnimationDuration);
            IsItCompartirColorButtonActive = false;
        }  
    }
}
