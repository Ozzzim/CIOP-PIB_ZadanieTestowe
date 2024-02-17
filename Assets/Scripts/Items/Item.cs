using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string[] properties;//UNUSED In future could be utilized for simple checks
    public ItemType itemType;//Where item can be equipped
    public Sprite thumbnail;

    public GameObject[] objectsToSpawn;//Objects spawned on equiping
}

public enum ItemType{
    TOOL,
    HELMET,
    GLOVES,
    BOOTS,
    VEST,
    BELT,
    MASK,
    NONE
}
