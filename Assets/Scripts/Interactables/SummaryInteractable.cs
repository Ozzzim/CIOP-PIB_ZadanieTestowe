using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryInteractable : Interactable
{
    protected override void Interact(){
        
        ScoreLogger.CreateSummaryScreen("Finished");
        //ScoreLogger.PrintAllMistakes();
    }
}
