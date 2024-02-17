using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pueExitCC : PhysUIElement
{
    public Color defaultColor;
    public Color highlightColor;
    [Header("References")]
    [SerializeField]
    private CharacterCustomization cc;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public override void OnPress(LaserPointer lp){
        cc.Close();
        Player.PlaySound(1);
    }
    public override void OnFloatOver(){
        spriteRenderer.color = highlightColor;
    }
    public override void OnFloatOut(){
        spriteRenderer.color = defaultColor;
    }
}
