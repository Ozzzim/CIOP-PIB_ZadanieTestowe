using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{

    [Header("References")]
    [SerializeField]
    private Door door;
    [SerializeField]
    private DoorHazard doorHazard;

    protected override void Interact(){
        if(!door.IsMoving()){
            if(doorHazard.gameObject.activeSelf){//If door haven't been disarmed properly
                if(doorHazard.GetHazardLevel()>0){//If it wasn't disarmed at all
                    door.Open(2);
                    door.Explode();
                    Player.OnMistake("Opened "+ door.name +" before disarming in "+doorHazard.GetRoom().name+".");
                } else {//If it wasn't scanned at the end
                    Player.OnMistake("Did not scan "+ door.name +" after disarming in "+doorHazard.GetRoom().name+".");
                    door.Open();
                }
                doorHazard.Broken();
                doorHazard.gameObject.SetActive(false);
            } else {//Regular case
                if(door.IsOpen()){
                    door.Close();
                } else {
                    door.Open();
                }
            }
        }
    }
}
