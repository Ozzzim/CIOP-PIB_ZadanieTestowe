using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : MonoBehaviour
{
    protected Item item;
    [SerializeField]
    protected Transform[] attachedTransforms;//Transforms objects created by Item are attached to
    protected List<GameObject> instantiated = new List<GameObject>();//List instantiated objects created by the Item
    
    [SerializeField]
    private ItemType itemType;

    //Activated when Item is equipped
    public virtual void SetItem(Item newitem){
        ClearItem();
        item = newitem;
        for(int i=0; i < attachedTransforms.Length && i < item.objectsToSpawn.Length; i++){//attachedTransforms length has to match objectsToSpawn length or not everything will be instantiated
            instantiated.Add(Instantiate(item.objectsToSpawn[i],attachedTransforms[i]));//Instantiate object, attach it the transform and add to instantiated list
        }
    }

    public virtual void ClearItem(){
        if(item){
            foreach(GameObject go in instantiated){
                Destroy(go);
            }
            instantiated = new List<GameObject>();//Clear the list
            item = null;
        }
    }

    public bool HasItem(){ return item != null; }
    public ItemType GetItemType(){ return itemType;}
    //Hides renderers of instantiated objects. Any other scripts will still be active
    public void SetVisibility(bool visible){
        Renderer r;
        foreach(GameObject go in instantiated){
            r = go.GetComponent<Renderer>();
            if(r)
                r.enabled = visible;
            r = null;
        }
    }
}
