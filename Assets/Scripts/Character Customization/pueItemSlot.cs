using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pueItemSlot : PhysUIElement
{   
    public Color highlightColor;
    private Color regularColor;
    public Item item;

    [Header("References")]
    [SerializeField]
    private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField]
    private SpriteRenderer itemThumbnailSpriteRenderer;
    [SerializeField]
    private TextMeshPro label;
    
    void Start(){
        regularColor = backgroundSpriteRenderer.color;
        if(item){
            itemThumbnailSpriteRenderer.sprite = item.thumbnail;
            label.text = item.name;
        }
    }

    public override void OnFloatOver(){
        backgroundSpriteRenderer.color = highlightColor;
    }
    public override void OnFloatOut(){
        backgroundSpriteRenderer.color = regularColor;
    }

    public override void OnPress(LaserPointer lp){
        if(item){
            lp.StoreItem(item);
            Player.PlaySound(1);
        }
    }

}
