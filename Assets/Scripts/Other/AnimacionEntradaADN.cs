using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionEntradaADN : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imagen;
    public GameObject animationManager;
    private void OnEnable()
    {
        animationManager.GetComponent<UIAniManager>().ScaleAnimationEnter(imagen);
    }
}
