using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DescubrirButton : MonoBehaviour
{
    public GameObject HeaderBackground;
    public GameObject ResultadosBusqueda;
    public GameObject HeaderRestPosition;
    public GameObject HeaderPosPosition;
    public GameObject BuscadorBackground;
    public GameObject CancelarButton;
    public GameObject BuscarRestPosition;
    public GameObject BuscarPosPosition;

    public void OnClick_BuscarButton(GameObject ScrollView){
        
        UIAniManager.instance.VerticalTransitionToCustomPosition(HeaderBackground, HeaderPosPosition,ScrollView, true );
        BuscadorBackground.transform.DOMove(BuscarPosPosition.transform.position, 0.5f).OnComplete(() => {CancelarButton.SetActive(true);UIAniManager.instance.FadeIn(ScrollView, .3f);});
        BuscadorBackground.transform.DOScale(new Vector3(0.7f,1,1), 0.5f);
        UIAniManager.instance.FadeIn(ResultadosBusqueda, 0.5f);
    }

    public void OnClick_CancelarButton(GameObject ScrollView){

            UIAniManager.instance.FadeOut(ScrollView, 0.5f);
            UIAniManager.instance.VerticalTransitionToCustomPosition(HeaderBackground, HeaderRestPosition, ScrollView, false);
            UIAniManager.instance.VerticalTransitionToCustomPosition(BuscadorBackground, BuscarRestPosition,CancelarButton, false );
            UIAniManager.instance.FadeOut(ResultadosBusqueda, 0.5f);
            BuscadorBackground.transform.DOScale(new Vector3(1,1,1), 0.5f);
    }
}
