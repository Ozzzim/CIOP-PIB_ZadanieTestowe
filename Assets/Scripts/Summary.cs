using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Summary : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI summaryTitle;
    [SerializeField]
    private TextMeshProUGUI mistakesList;
    [SerializeField]
    private TextMeshProUGUI playData;
    public void SetData(string title, List<string> mistakes, float time, int rooms, int hazardsTotal, int hazardsCleared, int overshotWallsCount){
        summaryTitle.text = title;
        playData.text = "Time: "+time.ToString("0.##s")
                        +"\nRooms: "+rooms
                        +"\nWalls overtreated: "+overshotWallsCount
                        +"\nHazards: "+hazardsCleared+"/"+hazardsTotal
                        +"\nGrade: "+GetGrade(hazardsTotal,hazardsCleared);
        foreach(string s in mistakes)
            mistakesList.text+=("- "+s+"\n");
    }

    //public void 

    private string GetGrade(int hazardsTotal, int hazardsCleared){
        if(hazardsTotal == 0)
            return "N/A";
        int result = (int)((float)hazardsCleared/hazardsTotal * 100);
        switch(result){
            case int i when i>=96:
                return "A";
            case int i when i>=90:
                return "B";
            case int i when i>=73:
                return "C";
            case int i when i>=50:
                return "D";
            case int i when i>=30:
                return "E";
            default:
                return "F";
        }
    }

    public void ScrollTest(){
        Debug.Log("Skoll");
    }
}
