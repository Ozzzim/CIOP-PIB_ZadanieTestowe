using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHazard : Hazard
{

    [SerializeField]//Remove later
    protected int hazardLevel = 0;
    private bool exploded = false;
    
    [Header("References")]
    [SerializeField]
    private GameObject checkmark;
    [SerializeField]
    private Door door;

    public void Start(){
        hazardLevel = Random.Range(1,9);
    }
    public override void OnFix(int hazardSetting){
        if(hazardLevel == hazardSetting && hazardLevel > 0)
            hazardLevel = 0;
        else {
            door.Explode();
            door.Open(2);
            if(hazardSetting > hazardLevel)
                Player.OnMistake("Tool setting too high on "+ door.name +" in "+attachedRoom.name+".");
            else
                Player.OnMistake("Tool setting too low on "+ door.name +" in "+attachedRoom.name+".");
            gameObject.SetActive(false);
            exploded = true;
        }
    }
    public override int OnScan(){ 
        if(hazardLevel == 0 ){
            active = false;
            gameObject.SetActive(false);
            checkmark.SetActive(true);
            
        }
        return hazardLevel; 
    }
    public override void GetRemainingMistakes(){
        if(active && !exploded)
            Player.OnMistake("Did not disarm "+door.name+" in "+attachedRoom.name+".", false, true);
    }
    public int GetHazardLevel(){ return hazardLevel; }
    public void Exploded(){exploded = true;}
}
