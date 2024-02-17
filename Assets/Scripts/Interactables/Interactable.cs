using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool repeatable = false;//Can Interactable be reused by default
    private bool used = false;

    //Interactable wrapper
    public virtual void OnInteraction(){
        if(!used || repeatable){
            used = true;
            Player.PlaySound(1);
            Interact();
        }
    }

    public void ResetUse(){ used = false; }
    
    //Main operation
    protected abstract void Interact();
}
