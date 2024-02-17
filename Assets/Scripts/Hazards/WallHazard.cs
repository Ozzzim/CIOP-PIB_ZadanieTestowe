using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHazard : Hazard
{
    private int overshot = 0;

    [Header("References")]
    [SerializeField]
    private GameObject checkmark;
    public override void OnFix(int hazardSetting){
        if(active){
            if(hazardSetting > 0){
                Player.OnMistake("Wrong tool setting on "+ name +" in "+attachedRoom.name+".");
            } else{
                active = false;
                checkmark.SetActive(true);
            }
        } else {
            Player.PlaySound(0);
            overshot++;
        }
            
    }
    public override void GetRemainingMistakes(){
        if(active)
            Player.OnMistake("Did not disarm "+name+" in "+attachedRoom.name+".", false, true);
        if(overshot>0)
            Player.OnMistake("Used tool "+ overshot +" "+(overshot>1 ? "times" : "time")+" too many on "+ name +" in "+attachedRoom.name+".",false, true); 
    }
    public override int OnScan(){ return 0; }
    public int GetOvershotCount(){ return overshot;}
}
