using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshPro roomLabel;
    [SerializeField]
    private Hazard[] hazards;
    
    void Start()
    {
        roomLabel.text = name;
        foreach(Hazard h in hazards)
            h.SetRoom(this);
    }

    public void CheckForRemainingHazards(){
        foreach(Hazard h in hazards){
            h.GetRemainingMistakes();
            /*if(h.GetStatus()){
                Player.OnMistake("Did not disarm "+h.name+" in "+name+".", false, true);
            }*/
        }
    }

    public int GetOvershotDoorCount(){
        int count = 0;
        foreach(Hazard h in hazards){
            if(h is WallHazard && ((WallHazard)h).GetOvershotCount()>0)
                count++;
        }
        return count;
    }

    public int HazardCount(){
        return hazards.Length;
    }
    public int ClearedHazardCount(){
        int count = 0;
        foreach(Hazard h in hazards){
            if(!h.GetStatus())
                count++;
        }
        return count;
    }
}
