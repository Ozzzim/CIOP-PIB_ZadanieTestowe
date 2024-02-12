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
            if(doorHazard.gameObject.activeSelf){//GetStatus()){
                if(doorHazard.GetHazardLevel()>0){
                    door.Open(2);
                    door.Explode();
                    doorHazard.Exploded();
                    Player.OnMistake("Opened "+ door.name +" before disarming in "+doorHazard.GetRoom().name+".");
                } else {
                    Player.OnMistake("Did not scan "+ door.name +" after disarming in "+doorHazard.GetRoom().name+".");
                    door.Open();
                }
                doorHazard.gameObject.SetActive(false);
            } else {
                if(door.IsOpen()){
                    door.Close();
                } else {
                    door.Open();
                }
            }
        }
    }
}
