using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool open = false;
    private bool moving = false;
    
    [Header("References")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ParticlesController explosionEffect;

    public bool IsOpen(){ return open; }
    public bool IsMoving(){ return moving; }
    
    //Plays opening animation;
    public void Open(float speed = 1){
        open = true;
        moving = true;
        animator.speed = speed;
        animator.Play("DoorOpen");
    }

    //Plays closing animation
    public void Close(float speed = 1){
        open = false;
        moving = true;
        animator.speed = speed;
        animator.Play("DoorClose");
    }

    public void Explode(){
        explosionEffect.Play();
    }

    //Called at the end of animation
    public void AnimationStop(){
        moving = false;
        animator.speed = 1;
    }
}
