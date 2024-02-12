using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolScanner : Tool
{
    public float range = 4;
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI readingOutput;
    [SerializeField]
    private Transform raycastOrigin;
    [SerializeField]
    private AudioSource audioSource;

    public override void Use(){
        int layerMask = LayerMask.GetMask("Default", "Hazard");//1 << 6;
        RaycastHit hit;
        if (Physics.Raycast(    raycastOrigin.position,
                                raycastOrigin.forward,//TransformDirection(Vector3.forward),
                                out hit, 
                                range, 
                                layerMask))
        {
            Hazard h = hit.transform.GetComponent<Hazard>();
            if(h){
                string output = h.OnScan()+"";
                Debug.Log("Scan of "+hit.transform.name+":"+output);
                readingOutput.text = output;
                audioSource.Play();
            } else {
                Debug.Log("Scan of "+hit.transform.name);
                readingOutput.text = "-";
            }
            
        }
    }
}
