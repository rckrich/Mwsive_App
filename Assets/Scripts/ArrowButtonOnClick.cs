using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButtonOnClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public int count = 0;
    public GameObject opciones;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimatorOnClick()
    {
        if (count == 1)
        {
            animator.Play("ArrowSettingsPlay2_Anim");
            opciones.SetActive(false);
            count--;
            
        }
        else
        {
            animator.Play("ArrowSettingsPlay_Anim");
            count++;
            opciones.SetActive(true);
        }
        
    }
}
