using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    protected bool active = true;
    protected Room attachedRoom;

    public abstract void OnFix(int hazardSetting);
    //public abstract void OnClear();
    //public abstract void OnMistake();
    public abstract int OnScan();
    public virtual bool GetStatus(){
        return active;
    }
    public void SetRoom(Room attachedRoom){
        if(!this.attachedRoom)
            this.attachedRoom = attachedRoom;
    }
    public abstract void GetRemainingMistakes();

    public Room GetRoom(){ return attachedRoom; }
}
