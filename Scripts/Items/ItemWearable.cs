using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "ItemWearable", menuName="InventorySystem/Items/ItemWearable")]
public class ItemWearable : Item
{
    public void Awake(){
        type = ItemType.Wearable;
    }
    
}
