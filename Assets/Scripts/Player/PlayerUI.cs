using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private Image[] chances;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer && !Global.IsGamePaused())
            timer.text = ScoreLogger.GetTime().ToString("0.##s");
    }

    public void SetChances(int chanceCount){
        for(int i = 0; i < chances.Length; i++)
            chances[i].enabled = (i+1 <= chanceCount);
    }
}
