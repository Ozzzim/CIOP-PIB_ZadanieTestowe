using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLogger : MonoBehaviour
{
    private static ScoreLogger instance;
    public bool timerOn = false;
    private float timer;
    private List<string> mistakes;
    
    [Header("References")]
    [SerializeField]
    private List<Room> rooms;
    [SerializeField]
    private Summary summaryScreen;
    void Start()
    {
        if(instance){
            Destroy(this.gameObject);
        } else {
            instance = this;
            mistakes = new List<string>();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn && !Global.IsGamePaused())
            timer += Time.deltaTime;
    }
    public static ScoreLogger GetInstance(){
            if(instance)
                return instance;
            return null;
    }

    public static void AddMistake(string mistakeText){
        //Debug.Log(mistakeText);
        if(instance)
            instance.mistakes.Add(mistakeText);
    }    
    public static List<string> GetMistakes(){
        if(instance)
            return instance.mistakes;
        return null;
    }
    public static float GetTime(){
        if(instance)
            return instance.timer;
        return -1;
    }
    public static void PrintAllMistakes(){
        foreach(string s in instance.mistakes)
            Debug.Log(s);
    }

    public static void CreateSummaryScreen(string title){
        int hazardCount = 0;
        int clearedHazardCount = 0;
        int overshotWallsCount = 0;
        foreach(Room r in instance.rooms){
            r.CheckForRemainingHazards();
            overshotWallsCount += r.GetOvershotDoorCount();
            hazardCount += r.HazardCount();
            clearedHazardCount += r.ClearedHazardCount();
        }
        Instantiate(instance.summaryScreen).SetData(title, instance.mistakes, GetTime(), instance.rooms.Count, hazardCount, clearedHazardCount, overshotWallsCount);
        
        Player.SetUI(false);
        Global.SetPause(true);
    }
}
