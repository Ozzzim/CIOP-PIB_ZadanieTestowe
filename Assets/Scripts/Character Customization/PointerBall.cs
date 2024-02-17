using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBall : MonoBehaviour
{
    PhysUIElement pue;
    public void OnTriggerEnter(Collider c){
        pue = c.GetComponent<PhysUIElement>();
        if(pue){
            pue.OnFloatOver();
        }
    }
    
    public void OnTriggerExit(Collider c){
        pue = c.GetComponent<PhysUIElement>();
        if(pue){
            pue.OnFloatOut();
        }
    }
}
