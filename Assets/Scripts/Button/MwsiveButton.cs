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

    public void OnClickOlaButton(){
        Debug.Log("AA");
        if(!IsItOlaColorButtonActive){
            UIAniManager.instance.ScaleAnimationEnter(OlaColorButton);
            IsItOlaColorButtonActive = true;
        }else{
            UIAniManager.instance.ScaleAnimationExit(OlaColorButton);
            IsItOlaColorButtonActive = false;
        }  
    }

    public void OnClickAñadirButton(){
        if(!IsItAñadirColorButtonActive){
            UIAniManager.instance.ScaleAnimationEnter(AñadirColorButton);
            IsItAñadirColorButtonActive = true;
        }else{
            UIAniManager.instance.ScaleAnimationExit(AñadirColorButton);
            IsItAñadirColorButtonActive = false;
        }  
    }


    public void OnClickCompartirButton(){
        if(!IsItCompartirColorButtonActive){
            UIAniManager.instance.ScaleAnimationEnter(CompartirColorButton);
            IsItCompartirColorButtonActive = true;
        }else{
            UIAniManager.instance.ScaleAnimationExit(CompartirColorButton);
            IsItCompartirColorButtonActive = false;
        }  
    }
}
