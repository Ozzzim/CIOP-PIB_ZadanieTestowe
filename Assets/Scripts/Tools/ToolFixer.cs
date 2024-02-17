using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolFixer : Tool
{
    public float range = 4;
    private int hazardSetting = 0;
    [Header("References")]
    [SerializeField]
    private Transform raycastOrigin;
    [SerializeField]
    private AudioSource audioSource_FixSound;
    [SerializeField]
    private AudioSource audioSource_Knobturn;
    [SerializeField]
    private Transform settingKnob;

    public override void Use(){
        int layerMask = LayerMask.GetMask("Default", "Hazard");
        RaycastHit hit;
        if (Physics.Raycast(    raycastOrigin.position,
                                raycastOrigin.forward,
                                out hit, 
                                range, 
                                layerMask))
        {
            Hazard h = hit.transform.GetComponent<Hazard>();
            if(h){
                h.OnFix(hazardSetting);
            }
            audioSource_FixSound.Play();
        }
    }

    
    void Update()
    {
        if(!Global.IsGamePaused())
            ScrollWheelAction(Input.GetAxis("Mouse ScrollWheel"));
    }

    //Scrolling controls
    public void ScrollWheelAction(float input){
        if(Mathf.Abs(input) > 0f){
            if(input > 0){
                if(hazardSetting < 9){
                    hazardSetting++;
                    RotateKnob(1);
                    audioSource_Knobturn.Play();
                }
            } else {
                if(hazardSetting > 0){
                    hazardSetting--;
                    RotateKnob(-1);
                    audioSource_Knobturn.Play();
                }
            }
        }
    }
    
    //Visual rotation of the dial
    private void RotateKnob(int direction){
        Vector3 rotation = settingKnob.localEulerAngles;
        rotation.y -= direction * 36;
        settingKnob.localEulerAngles = rotation;
    }
}
