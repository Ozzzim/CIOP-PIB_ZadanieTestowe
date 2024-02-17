using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysUIElement : MonoBehaviour
{
    public virtual void OnFloatOver(){}
    public virtual void OnFloatOut(){}
    //Laserpointer is necessary for processing movement of items
    public virtual void OnPress(LaserPointer lp){}
    public virtual void OnRelease(LaserPointer lp){}
}
