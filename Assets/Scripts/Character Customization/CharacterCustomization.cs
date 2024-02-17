using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCustomization : MonoBehaviour
{
    
    public puePlayerSlot[] playerSlots;

    [Header("References")]
    [SerializeField]
    private Player customizedPlayer;
    void Start()
    {
        Global.SetPause(true, false);
    }
    
    //Highlights slots item can be dropped in
    public void ShowAvailability(ItemType it){
        foreach(puePlayerSlot pps in playerSlots){
            pps.SetAvailability(it == pps.GetItemType());
        }
    }

    //Sets slots colors to default
    public void HideAvailability(){
        foreach(puePlayerSlot pps in playerSlots)
            pps.SetAvailability(false);
    }

    public void Close(){
        Destroy(this.gameObject);
        Global.SetPause(false, false);
        Player.SetUI(true);
        Player.HideObscuringEquipment(true);
    }
}

