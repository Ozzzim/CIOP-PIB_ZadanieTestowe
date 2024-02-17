using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puePlayerSlot : PhysUIElement
{
    public Color availableColor;
    public Color defaultColor;

    private Item item;
    [Header("References")]
    [SerializeField]
    private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField]
    private SpriteRenderer itemThumbnailSpriteRenderer;
    [SerializeField]
    private PlayerSlot slot;
    public override void OnPress(LaserPointer lp){
        Player.PlaySound(0);
        ClearSlot();
        ClearItem();
    }
    public override void OnRelease(LaserPointer lp){
        if(lp.HasItem() && lp.StoredItemType() == slot.GetItemType()){
            item = lp.ClearItem();
            SetItem();
            Player.PlaySound(1,0.8f);
        }
    }

    //Relays item to PlayerSlot
    protected virtual void SetItem(){
        slot.SetItem(item);
        itemThumbnailSpriteRenderer.sprite = item.thumbnail;
    }

    protected virtual void ClearItem(){
        slot.ClearItem();
        Player.PlaySound(1);
    }

    private void ClearSlot(){
        item = null;
        itemThumbnailSpriteRenderer.sprite = null;
    }

    public void SetAvailability(bool availability){
        if(availability)
            backgroundSpriteRenderer.color = availableColor;
        else
            backgroundSpriteRenderer.color = defaultColor;
    }

    public ItemType GetItemType(){ 
        if(slot)
            return slot.GetItemType();
        return ItemType.NONE;
    }
}
