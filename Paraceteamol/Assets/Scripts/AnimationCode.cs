using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCode : MonoBehaviour
{
    private Animator animator;
    public bool movendo = false;
    public bool pulando = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Andar()
    {

        animator.SetBool("movendo", true);

    }
    public void PararDeAndar()
    {
        animator.SetBool("movendo", false);
    }



    public void Pular()
    {

        animator.SetTrigger("pulando");

    }
    public void PararDePular()
    {
        animator.ResetTrigger("pulando");

    }
}