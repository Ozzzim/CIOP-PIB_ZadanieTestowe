using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolSlot : PlayerSlot
{
    //PlayerTool variant with support to assign control to the tools
    public Mapping map;
    public override void SetItem(Item newitem){
        ClearItem();
        item = newitem;
        for(int i=0; i < attachedTransforms.Length && i < item.objectsToSpawn.Length; i++){
            instantiated.Add(Instantiate(item.objectsToSpawn[i],attachedTransforms[i]));
        }
        if(instantiated.Count>0){//Pick first spawned object and if possible attach it to the Player controls
            Tool t = instantiated[0].GetComponent<Tool>();
        
            switch(map){
                case Mapping.LEFT:
                    Player.GetPlayer().leftTool = t;
                    break;
                case Mapping.RIGHT:
                    Player.GetPlayer().rightTool = t;
                    break;
                default:
                    break;
            }
        }
    }

    public override void ClearItem(){
        if(item){
            switch(map){
                case Mapping.LEFT:
                    Player.GetPlayer().leftTool = null;
                    break;
                case Mapping.RIGHT:
                    Player.GetPlayer().rightTool = null;
                    break;
                default:
                    break;
            }
            foreach(GameObject go in instantiated){
                Destroy(go);
            }
            instantiated = new List<GameObject>();
            item = null;
        }
    }
}

public enum Mapping{
    LEFT,
    RIGHT
}
