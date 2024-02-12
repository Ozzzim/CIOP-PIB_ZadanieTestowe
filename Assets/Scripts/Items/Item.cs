using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string transformName;
    public abstract void OnEquip();
    public abstract void OnDeequip();
}
