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

    //Checks hazards for any remaining mistakes at the end of the session
    public void CheckForRemainingHazards(){
        foreach(Hazard h in hazards){
            h.GetRemainingMistakes();
        }
    }

    //Returns number of walls that have been overfixed
    public int GetOvershotWallsCount(){
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
