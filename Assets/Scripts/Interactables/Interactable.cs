using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool repeatable = false;
    private bool used = false;

    public virtual void OnInteraction(){
        if(!used || repeatable){
            used = true;
            Player.PlaySound(1);
            Interact();
        }
    }

    public void ResetUse(){ used = false; }

    protected abstract void Interact();
}
