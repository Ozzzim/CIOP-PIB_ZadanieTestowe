using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pueDrawer : PhysUIElement
{
    public Color highlightColor;
    private Color regularColor;
    public float itemSpacing = 1f;
    public float expansionTime = 0.5f;
    bool expanded = false; 
    public IEnumerator expandCoroutine;

    [Header("References")]
    [SerializeField]
    private Transform[] items;
    [SerializeField]
    private pueDrawer[] paralellDrawers;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    void Start(){
        regularColor = spriteRenderer.color;
    }

    public override void OnFloatOver(){
        spriteRenderer.color = highlightColor;
        Expand();
    }
    public override void OnFloatOut(){
        spriteRenderer.color = regularColor;
    }

    public override void OnPress(LaserPointer lp){
        Withdraw();
        base.OnPress(lp);
    }

    //Expands the drawer
    public void Expand(){
        if(!expanded && expandCoroutine == null){
            if(items.Length > 0){
                expandCoroutine = CoExpand(expansionTime);
                StartCoroutine(expandCoroutine);
            } else
                CloseOtherDrawers();
            
            expanded = true;
        }
    }

    //Expands the drawer, but without animating coroutine
    public void ExpandInstant(){
        if(!expanded){
            for(int i=0; i < items.Length; i++){
                    items[i].position = transform.position + transform.right * itemSpacing * (i+1);
                    items[i].GetComponent<Collider>().enabled = true;
            }
        }
    }

    //Withdraws drawer elements
    public void Withdraw(){
        if(expanded){
            foreach(Transform t in items){
                t.GetComponent<Collider>().enabled = false;
                t.localPosition = Vector3.zero;
            }
            expanded = false;
        }
    }

    public bool IsExpanded(){
        return expanded;
    }
    
    //Animating coroutine
    public IEnumerator CoExpand(float time){
        if(time == 0)
            yield break;

        float timer = 0;
        float timerPerc = 0;

        while(timer < expansionTime){
            for(int i=0; i < items.Length; i++){
                items[i].position = Vector3.Lerp(transform.position,transform.position + transform.right * itemSpacing * (i+1),timerPerc);
            }
            timer+=Time.deltaTime;
            timerPerc = timer/time;
            yield return null;
        }
        //Turns on colliders on elements
        foreach(Transform t in items){
            t.GetComponent<Collider>().enabled = true;
        }
        expandCoroutine = null;
        CloseOtherDrawers();
    }

    //Signal sent to other drawers to withdraw 
    private void CloseOtherDrawers(){
        foreach(pueDrawer pd in paralellDrawers){
            //Stop any active coroutines
            if(pd.expandCoroutine != null){
                StopCoroutine(pd.expandCoroutine);
                pd.expandCoroutine = null;
            }
            pd.Withdraw();
        }
    }
}
